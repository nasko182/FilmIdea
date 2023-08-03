namespace FilmIdea.Services.Data;

using Microsoft.EntityFrameworkCore;

using Interfaces;
using FilmIdea.Data;
using FilmIdea.Data.Models;
using FilmIdea.Data.Models.Join_Tables;
using Models.Movies;
using Web.ViewModels.Actor;
using Web.ViewModels.Comment;
using Web.ViewModels.Director;
using Web.ViewModels.Genre;
using Web.ViewModels.Movie;
using Web.ViewModels.Movie.Enums;
using Web.ViewModels.Review;

public class MovieService : FilmIdeaService, IMovieService
{
    private readonly FilmIdeaDbContext _dbContext;
    private readonly Random _random;

    public MovieService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
        this._random = new Random();
    }

    public async Task<MoviesFilteredAndPagesServiceModel> AllAsync(MoviesQueryModel queryModel, string userId)
    {
        var moviesQuery = this._dbContext.Movies
            .Include(m => m.Actors)
            .ThenInclude(ma => ma.Actor)
            .Include(m => m.Genres)
            .ThenInclude(g => g.Genre)
            .Include(m => m.Ratings)
            .Include(m=>m.UsersWatchlists)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryModel.Genre))
        {
            moviesQuery = moviesQuery
                .Where(m => m.Genres.Any(g => g.Genre.Name == queryModel.Genre));
        }
        if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
        {
            string wildCard = $"%{queryModel.SearchString.ToLower()}%";

            moviesQuery = moviesQuery
                .Where(m =>
                    EF.Functions.Like(m.Title, wildCard) ||
                    EF.Functions.Like(m.Director.Name, wildCard) ||
                    m.Genres.Select(g => g.Genre.Name).Any(gn => EF.Functions.Like(gn, wildCard) ||
                    m.Actors.Select(a => a.Actor.Name).Any(a => EF.Functions.Like(a, wildCard)))
                    );
        }

        moviesQuery = queryModel.MovieSorting switch
        {
            MovieSorting.Newest => moviesQuery
                .OrderBy(m => m.ReleaseDate),
            MovieSorting.Oldest => moviesQuery
                .OrderByDescending(m => m.ReleaseDate),
            MovieSorting.RatingAscending => moviesQuery
                .OrderBy(m => m.Ratings.Average(r => r.Rating)),
            MovieSorting.RatingDescending => moviesQuery
                .OrderByDescending(m => m.Ratings.Average(r => r.Rating)),
            _ => moviesQuery.OrderBy(m => m.Id)
        };

        var userRatings = await this._dbContext.UserRatings
            .Where(r => r.UserId.ToString() == userId)
            .Select(r => new UserRating()
            {
                Rating = r.Rating,
                MovieId = r.MovieId
            })
            .ToListAsync();

        ICollection<MovieViewModel> allMovies = await moviesQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.MoviesPerPage)
            .Take(queryModel.MoviesPerPage)
            .Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                CoverPhotoUrl = m.CoverImageUrl,
                ReleaseYear = m.ReleaseDate.Year,
                UserRating = GetRating(userRatings, m.Id),
                Duration = m.Duration,
                Rating = m.CalculateUserRating(),
                HasMovieInWatchlist = HasMovieInUserWatchlist(userId, m)
            })
            .ToArrayAsync();

        var totalMovies = moviesQuery.Count();

        return new MoviesFilteredAndPagesServiceModel()
        {
            TotalMoviesCount = totalMovies,
            Movies = allMovies
        };

    }

    public async Task<bool> IsGenreIdValid(int genreId)
    {
        return await this._dbContext.Genres
            .AnyAsync(g => g.Id == genreId);
    }

    public async Task<string?> GetGenreNameByIdAsync(int genreId)
    {
        return await this._dbContext.Genres
            .Where(g => g.Id == genreId)
            .Select(g => g.Name)
            .FirstOrDefaultAsync();
    }

    public async Task<MoviesAndTopViewModel> GetAllMoviesAsync(string? userId)
    {
        var n = await this.GetMoviesForTopSectionAsync();
        return new MoviesAndTopViewModel()
        {
            TopMovies = await this.GetMoviesForTopSectionAsync(),
            Movies = await this.GetMoviesForAllAsync(userId)
        };

    }

    public async Task<MoviesAndTopViewModel> GetNewMoviesAsync(string userId)
    {
        return new MoviesAndTopViewModel()
        {
            TopMovies = await this.GetMoviesForTopSectionAsync(),
            Movies = await this.GetMoviesForNewAsync(userId)
        };
    }

    public async Task<MoviesAndTopViewModel> GetTop250MoviesAsync(string userId)
    {
        return new MoviesAndTopViewModel()
        {
            TopMovies = await this.GetMoviesForTopSectionAsync(),
            Movies = await this.GetMoviesForTop250Async(userId)
        };
    }

    public async Task<MoviesAndTopViewModel> GetMoviesByGenreAsync(string userId, int genreId)
    {
        return new MoviesAndTopViewModel()
        {
            TopMovies = await this.GetMoviesForTopSectionAsync(),
            Movies = await this.GetMoviesForGenreAsync(userId, genreId)
        };
    }

    public async Task<MovieDetailsViewModel?> GetMovieAsync(int movieId, string userId)
    {
        var userRatings = await this._dbContext.UserRatings
            .Where(r => r.UserId.ToString() == userId)
            .Select(r => new UserRating()
            {
                Rating = r.Rating,
                MovieId = r.MovieId
            })
            .ToListAsync(); ;

        var movie = await this._dbContext
            .Movies
            .Where(m => m.Id == movieId)
            .Include(m => m.Ratings)
            .Select(m => new MovieDetailsViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseDate.Year,
                CoverImageUrl = m.CoverImageUrl,
                Duration = m.Duration,
                Rating = m.CalculateUserRating(),
                TrailerUrl = m.TrailerUrl,
                Director = new DirectorNameAndIdViewModel()
                {
                    Id = m.Director.Id,
                    Name = m.Director.Name
                },
                Actors = m.Actors.Select(a => new ActorNameAndIdViewModel()
                {
                    Id = a.ActorId,
                    Name = a.Actor.Name
                }).ToList(),
                Genres = m.Genres.Select(mg => new GenreViewModel()
                {
                    Id = mg.GenreId,
                    Name = mg.Genre.Name
                }).ToList(),
                Reviews = m.Reviews.OrderBy(r => r.ReviewDate).Select(r => new ReviewViewModel()
                {
                    Title = r.Title,
                    MovieId = r.MovieId,
                    Id = r.Id.ToString(),
                    Content = r.Content,
                    CriticId = r.CriticId.ToString(),
                    CriticName = r.Critic.Name,
                    Rating = r.Rating,
                    Likes = r.Likes.Count,
                    Dislikes = r.Dislikes.Count,
                    ReviewDate = r.ReviewDate.ToString("MMM dd, yyyy"),
                    Comments = r.Comments.OrderBy(c => c.CommentDate).Select(c => new CommentViewModel()
                    {
                        Content = c.Content,
                        CommentDate = c.CommentDate.ToString("yyyy MM dd HH-mm"),
                        Id = c.Id.ToString(),
                        ReviewId = c.ReviewId.ToString(),
                        WriterId = c.WriterId.ToString(),
                        WriterName = c.Writer.Email.Substring(0, c.Writer.Email.IndexOf("@"))
                    }).ToList()
                }).ToList(),
                Photos = m.Photos.Select(p => p.Url).ToList(),
                Videos = m.Videos.Select(v => v.Url).ToList(),
                UserRating = GetRating(userRatings, movieId)
            })
            .FirstOrDefaultAsync();

        if (movie == null)
        {
            return null;
        }

        return movie;
    }

    public async Task<MovieViewModel> GetRouletteMovieAsync(string? userId)
    {
        if (userId != null)
        {
            var moviesCount = this._dbContext.Movies.Count();
            var movieIndex = this._random.Next(0, moviesCount);
            var userRatings = await this._dbContext.UserRatings
                .Where(r => r.UserId.ToString() == userId)
                .Select(r => new UserRating()
                {
                    Rating = r.Rating,
                    MovieId = r.MovieId
                })
                .ToListAsync();

            var movie = await this._dbContext
                .Movies
                .Skip(movieIndex)
                .Include(m => m.Ratings)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                    UserRating = GetRating(userRatings, m.Id)
                })
                .FirstAsync();

            return movie;
        }
        else
        {
            var moviesCount = this._dbContext.Movies.Count();
            var movieIndex = this._random.Next(0, moviesCount);

            var movie = await this._dbContext
                .Movies
                .Skip(movieIndex)
                .Include(m => m.Ratings)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                })
                .FirstAsync();

            return movie;
        }
    }

    public async Task<List<GenreViewModel>> GetAllGenresAsync()
    {
        return await this._dbContext.Genres.Select(g => new GenreViewModel()
        {
            Id = g.Id,
            Name = g.Name
        })
            .ToListAsync();
    }

    public async Task<List<MovieViewModel>> GetWatchlistMoviesAsync(string userId)
    {
        return await this._dbContext.UsersMovies
            .Include(um => um.Movie)
            .ThenInclude(m => m.UsersWatchlists)
            .Include(um => um.User)
            .ThenInclude(u => u.Ratings)
            .Where(um => um.UserId == Guid.Parse(userId))
            .Select(um => new MovieViewModel()
            {
                Id = um.Movie.Id,
                Rating = um.Movie.CalculateUserRating(),
                CoverPhotoUrl = um.Movie.CoverImageUrl,
                Duration = um.Movie.Duration,
                ReleaseYear = um.Movie.ReleaseDate.Year,
                Title = um.Movie.Title,
                UserRating = GetRating(um.User.Ratings, um.MovieId),
                HasMovieInWatchlist = HasMovieInUserWatchlist(userId, um.Movie),
            })
            .ToListAsync();
    }

    public async Task AddRatingAsync(int movieId, int ratingValue, string userId)
    {
        var movie = await this._dbContext.Movies
            .Include(m => m.Ratings)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie != null)
        {
            var userRating = movie.Ratings.FirstOrDefault(ur => ur.UserId.ToString() == userId && ur.MovieId == movieId);

            if (userRating != null)
            {
                userRating.Rating = ratingValue;
            }
            else
            {
                this._dbContext.UserRatings.Add(new UserRating()
                {
                    MovieId = movieId,
                    UserId = Guid.Parse(userId),
                    Rating = ratingValue
                });
            }

            try
            {
                await this._dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public async Task AddReviewAsync(AddReviewViewModel model, int movieId, string criticId)
    {
        var movie = await this._dbContext.Movies
            .Include(m => m.Reviews)
            .ThenInclude(r => r.Critic)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie != null)
        {
            await this._dbContext.Reviews.AddAsync(new Review()
            {
                Title = model.Title,
                MovieId = movieId,
                Rating = model.Rating,
                CriticId = Guid.Parse(criticId),
                Content = model.Content,
            });

            try
            {
                await this._dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public async Task AddCommentAsync(AddCommentViewModel model, string reviewId, string userId)
    {
        var review = await this._dbContext.Reviews
            .Where(r => r.Id.ToString() == reviewId)
            .Include(r => r.Comments)
            .FirstOrDefaultAsync();

        if (review != null)
        {
            await this._dbContext.Comments.AddAsync(new Comment()
            {
                Content = model.Content,
                ReviewId = Guid.Parse(reviewId),
                WriterId = Guid.Parse(userId)
            });

            try
            {
                await this._dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public async Task AddToUserWatchlist(string userId, int movieId)
    {
        var movie = await this._dbContext.Movies
            .Include(m => m.UsersWatchlists)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie != null)
        {

            if (!HasMovieInUserWatchlist(userId, movie))
            {
                movie.UsersWatchlists.Add(new UserMovie()
                {
                    UserId = Guid.Parse(userId),
                    MovieId = movieId
                });

                try
                {
                    await this._dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }

    public async Task RemoveFromUserWatchlist(string userId, int movieId)
    {
        var movie = await this._dbContext.Movies
            .Include(m => m.UsersWatchlists)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie != null)
        {

            if (HasMovieInUserWatchlist(userId, movie))
            {
                var userMovie = movie.UsersWatchlists.First(m => m.UserId == Guid.Parse(userId) && m.MovieId == movieId);

                movie.UsersWatchlists.Remove(userMovie);

                try
                {
                    await this._dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }

    public async Task AddRemoveLike(string reviewId, string userId)
    {
        var like = await this._dbContext.Likes
            .FirstOrDefaultAsync(l => l.UserId.ToString() == userId && l.ReviewId.ToString() == reviewId);

        if (like != null)
        {
            this._dbContext.Likes.Remove(like);
        }
        else
        {
            await this._dbContext.Likes.AddAsync(new Like()
            {
                ReviewId = Guid.Parse(reviewId),
                UserId = Guid.Parse(userId)
            });
        }

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddRemoveDislike(string reviewId, string userId)
    {
        var dislike = await this._dbContext.Dislikes
            .FirstOrDefaultAsync(dl => dl.UserId.ToString() == userId && dl.ReviewId.ToString() == reviewId);

        if (dislike != null)
        {
            this._dbContext.Dislikes.Remove(dislike);
        }
        else
        {
            await this._dbContext.Dislikes.AddAsync(new Dislike()
            {
                ReviewId = Guid.Parse(reviewId),
                UserId = Guid.Parse(userId)
            });
        }

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    private async Task<List<MovieViewModel>> GetMoviesForAllAsync(string? userId)
    {
        if (userId == null)
        {
            var movies = await this._dbContext
                .Movies
                .Include(m => m.Ratings)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                })
                .ToListAsync();

            return movies;
        }
        else
        {
            var userRatings = await this._dbContext.UserRatings
                .Where(r => r.UserId.ToString() == userId)
                .Select(r => new UserRating()
                {
                    Rating = r.Rating,
                    MovieId = r.MovieId
                })
                .ToListAsync();

            var movies = await this._dbContext
                .Movies
                .Include(m => m.Ratings)
                .Include(m => m.UsersWatchlists)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                    HasMovieInWatchlist = HasMovieInUserWatchlist(userId!, m),
                    UserRating = GetRating(userRatings, m.Id)
                })
                .ToListAsync();

            return movies;
        }
    }

    private async Task<List<MovieViewModel>> GetMoviesForNewAsync(string? userId)
    {
        if (userId == null)
        {
            var movies = await this._dbContext
                .Movies
                .Where(m => m.ReleaseDate.Year == DateTime.UtcNow.Year)
                .Include(m => m.Ratings)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year
                })
                .ToListAsync();

            return movies;
        }
        else
        {
            var userRatings = await this._dbContext.UserRatings
                .Where(r => r.UserId.ToString() == userId)
                .Select(r => new UserRating()
                {
                    Rating = r.Rating,
                    MovieId = r.MovieId
                })
                .ToListAsync();

            var movies = await this._dbContext
                .Movies
                .Where(m => m.ReleaseDate.Year == DateTime.UtcNow.Year)
                .Include(m => m.Ratings)
                .Include(m => m.UsersWatchlists)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                    HasMovieInWatchlist = HasMovieInUserWatchlist(userId!, m),
                    UserRating = GetRating(userRatings, m.Id)
                })
                .ToListAsync();

            return movies;
        }
    }

    private async Task<List<MovieViewModel>> GetMoviesForTop250Async(string? userId)
    {
        if (userId == null)
        {
            var movies = await this._dbContext
                .Movies
                .Include(m => m.Ratings)
                .OrderByDescending(m => m.Ratings.Average(mr => mr.Rating))
                .Take(250)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                    HasMovieInWatchlist = HasMovieInUserWatchlist(userId!, m),
                })
                .ToListAsync();

            return movies;
        }
        else
        {
            var userRatings = await this._dbContext.UserRatings
                .Where(r => r.UserId.ToString() == userId)
                .Select(r => new UserRating()
                {
                    Rating = r.Rating,
                    MovieId = r.MovieId
                })
                .ToListAsync();

            var movies = await this._dbContext
                .Movies
                .Include(m => m.Ratings)
                .Include(m => m.UsersWatchlists)
                .OrderByDescending(m => m.Ratings.Average(mr => mr.Rating))
                .Take(250)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                    HasMovieInWatchlist = HasMovieInUserWatchlist(userId!, m),
                    UserRating = GetRating(userRatings, m.Id)
                })
                .ToListAsync();

            return movies;
        }
    }

    private async Task<List<MovieViewModel>> GetMoviesForGenreAsync(string? userId, int genreId)
    {
        if (userId == null)
        {
            var movies = await this._dbContext
                .Movies
                .Where(m => m.Genres.Any(g => g.GenreId == genreId))
                .Include(m => m.Ratings)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                })
                .ToListAsync();

            return movies;
        }
        else
        {
            var userRatings = await this._dbContext.UserRatings
                .Where(r => r.UserId.ToString() == userId)
                .Select(r => new UserRating()
                {
                    Rating = r.Rating,
                    MovieId = r.MovieId
                })
                .ToListAsync();

            var movies = await this._dbContext
                .Movies
                .Where(m => m.Genres.Any(g => g.GenreId == genreId))
                .Include(m => m.Ratings)
                .Include(m => m.UsersWatchlists)
                .Select(m => new MovieViewModel()
                {
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    Rating = m.CalculateUserRating(),
                    ReleaseYear = m.ReleaseDate.Year,
                    HasMovieInWatchlist = HasMovieInUserWatchlist(userId!, m),
                    UserRating = GetRating(userRatings, m.Id)
                })
                .ToListAsync();

            return movies;
        }
    }

    public async Task<ICollection<TopSectionMovieViewModel>> GetMoviesForTopSectionAsync()
    {
        return await this._dbContext
            .Movies
            .OrderByDescending(m => m.ReleaseDate)
            .Take(10)
            .Select(m => new TopSectionMovieViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                PictureUrl = m.CoverImageUrl
            })
            .ToListAsync();
    }
}