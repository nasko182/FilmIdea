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
            .Include(m => m.UsersWatchlists)
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

        ICollection<MovieViewModel> allMovies;
        if (userId != null)
        {
            var userRatings = await this._dbContext.UserRatings
                .Where(r => r.UserId.ToString() == userId)
                .Select(r => new UserRating()
                {
                    Rating = r.Rating,
                    MovieId = r.MovieId
                })
                .ToListAsync();

            allMovies = await moviesQuery
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
        }
        else
        {
            allMovies = await moviesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.MoviesPerPage)
                .Take(queryModel.MoviesPerPage)
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    CoverPhotoUrl = m.CoverImageUrl,
                    ReleaseYear = m.ReleaseDate.Year,
                    Duration = m.Duration,
                    Rating = m.CalculateUserRating()
                })
                .ToArrayAsync();
        }

        var totalMovies = moviesQuery.Count();

        return new MoviesFilteredAndPagesServiceModel()
        {
            TotalMoviesCount = totalMovies,
            Movies = allMovies
        };

    }

    public async Task<bool> IsGenreIdValidAsync(int genreId)
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

    public async Task<MovieDetailsViewModel?> GetMovieDetailsAsync(int movieId, string userId)
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
                Reviews = m.Reviews.OrderByDescending(r => r.ReviewDate).Select(r => new ReviewViewModel()
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
                    AddReview = new AddReviewViewModel(),
                    AddComment = new AddCommentViewModel(),
                    Comments = r.Comments.OrderByDescending(c => c.CommentDate).Select(c => new CommentViewModel()
                    {
                        Content = c.Content,
                        CommentDate = TimeZoneInfo.ConvertTimeFromUtc(c.CommentDate, TimeZoneInfo.Local).ToString("dd.MM.yyyy HH:mm"),
                        Id = c.Id.ToString(),
                        ReviewId = c.ReviewId.ToString(),
                        WriterId = c.WriterId.ToString(),
                        WriterName = c.Writer.Email.Substring(0, c.Writer.Email.IndexOf("@")),
                        AddComment = new AddCommentViewModel()
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
            catch
            {
                throw new InvalidOperationException("Invalid operation");
            }
        }
    }

    public async Task EditReviewAsync(EditReviewViewModel model, string criticId)
    {
        var review = await this._dbContext.Reviews
            .Where(r => r.Id.ToString() == model.ReviewId)
            .FirstOrDefaultAsync();

        if (review != null && review.CriticId.ToString() == criticId)
        {

            review.Content = model.Content;
            review.Rating = model.Rating;
            review.Title = model.Title;

            try
            {
                await this._dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException("Invalid critic or review id.");
            }
        }
    }

    public async Task AddCommentAsync(AddCommentViewModel model, string userId)
    {
        var review = await this._dbContext.Reviews
            .Where(r => r.Id.ToString() == model.ReviewId)
            .Include(r => r.Comments)
            .FirstOrDefaultAsync();

        if (review != null)
        {
            await this._dbContext.Comments.AddAsync(new Comment()
            {
                Content = model.Content,
                ReviewId = Guid.Parse(model.ReviewId),
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

    public async Task EditCommentAsync(EditCommentViewModel model, string userId)
    {
        var comment = await this._dbContext.Comments
            .Where(c => c.Id.ToString() == model.CommentId)
            .FirstOrDefaultAsync();

        if (comment != null && comment.WriterId.ToString() == userId)
        {
            comment.Content = model.Content;
        }
        else
        {
            throw new ArgumentException("Invalid user or comment id");
        }

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new InvalidOperationException("Unexpected error!");
        }
    }

    public async Task AddToUserWatchlistAsync(string userId, int movieId)
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

    public async Task RemoveFromUserWatchlistAsync(string userId, int movieId)
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

    public async Task AddRemoveLikeAsync(string reviewId, string userId)
    {
        var like = await this._dbContext.Likes
            .FirstOrDefaultAsync(l => l.UserId.ToString() == userId && l.ReviewId.ToString() == reviewId);

        var dislike = await this._dbContext.Dislikes
            .FirstOrDefaultAsync(dl => dl.UserId.ToString() == userId && dl.ReviewId.ToString() == reviewId);

        if (dislike != null)
        {
            this._dbContext.Dislikes.Remove(dislike);
        }
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

    public async Task AddRemoveDislikeAsync(string reviewId, string userId)
    {
        var dislike = await this._dbContext.Dislikes
            .FirstOrDefaultAsync(dl => dl.UserId.ToString() == userId && dl.ReviewId.ToString() == reviewId);

        var like = await this._dbContext.Likes
            .FirstOrDefaultAsync(l => l.UserId.ToString() == userId && l.ReviewId.ToString() == reviewId);

        if (like != null)
        {
            this._dbContext.Likes.Remove(like);
        }
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

    public async Task DeleteReviewAsync(string reviewId)
    {
        var review = await this._dbContext.Reviews
            .Where(r => r.Id.ToString() == reviewId)
            .FirstAsync();

        this._dbContext.Comments.RemoveRange(review.Comments);
        this._dbContext.Reviews.Remove(review);

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new InvalidOperationException("Something went wrong!");
        }
    }

    public async Task DeleteCommentAsync(string commentId)
    {
        var comment = await this._dbContext.Comments
            .Where(c => c.Id.ToString() == commentId)
            .FirstAsync();

        this._dbContext.Comments.Remove(comment);

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new InvalidOperationException("Something went wrong!");
        }
    }

    public async Task<bool> IsCriticOwnerOfReview(string? criticId, string? reviewId)
    {

        if (reviewId == null)
        {
            return false;
        }
        var critic = await this._dbContext.Critics
            .Where(c => c.Id.ToString() == criticId)
            .Include(c => c.Reviews)
            .FirstOrDefaultAsync();

        if (critic == null)
        {
            return false;
        }
        return critic.Reviews.Any(r => r.Id.ToString() == reviewId.ToLower());
    }

    public async Task<bool> IsUserOwnerOfComment(string? userId, string? commentId)
    {
        if (commentId == null)
        {
            return false;
        }
        var comment = await this._dbContext.Comments
            .Where(c => c.Id.ToString() == commentId.ToLower())
            .FirstOrDefaultAsync();

        if (comment == null)
        {
            return false;
        }

        return comment.WriterId.ToString() == userId;
    }

    public async Task<EditMovieViewModel> GetMovieForEditByIdAsync(int id)
    {
        var movie = await this._dbContext.Movies
            .Where(m => m.Id == id)
            .FirstAsync();

        return new EditMovieViewModel
        {
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            Duration = movie.Duration,
            CoverImageUrl = movie.CoverImageUrl,
            TrailerUrl = movie.TrailerUrl,
            DirectorId = movie.DirectorId
        };
    }

    public async Task EditMovieByIdAndModelAsync(int id, EditMovieViewModel model)
    {
        var movie = await this._dbContext.Movies
            .Where(m => m.Id == id)
            .FirstAsync();

        movie.Title = model.Title;
        movie.Description = model.Description;
        movie.ReleaseDate = model.ReleaseDate;
        movie.Duration = model.Duration;
        movie.CoverImageUrl = model.CoverImageUrl;
        movie.TrailerUrl = model.TrailerUrl;
        movie.DirectorId = model.DirectorId;

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch 
        {
            throw new InvalidOperationException();
        }
    }

    public async Task DeleteMovieByIdAsync(int id)
    {
        var movie = await _dbContext
            .Movies
            .Include(m => m.Reviews)
            .ThenInclude(r => r.Comments)
            .Include(m=>m.Actors)
            .Include(m=>m.Genres)
            .Include(m=>m.PassedUsers)
            .Include(m=>m.Ratings)
            .Include(m=>m.UsersWatchlists)
            .Include(m=>m.Photos)
            .Include(m=>m.Videos)
            .FirstAsync(m => m.Id == id);

        foreach (var review in movie.Reviews)
        {
            this._dbContext.Comments.RemoveRange(review.Comments);
        }

        this._dbContext.Reviews.RemoveRange(movie.Reviews);

        this._dbContext.MoviesActors.RemoveRange(movie.Actors);

        this._dbContext.MoviesGenres.RemoveRange(movie.Genres);

        this._dbContext.UserRatings.RemoveRange(movie.Ratings);

        this._dbContext.UsersMovies.RemoveRange(movie.UsersWatchlists);

        this._dbContext.PassedMovies.RemoveRange(movie.PassedUsers);

        this._dbContext.Photos.RemoveRange(movie.Photos);

        this._dbContext.Videos.RemoveRange(movie.Videos);

        _dbContext.Remove(movie);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> GetMovieIdByReviewId(string? reviewId)
    {
        var review = await this._dbContext.Reviews
            .FirstOrDefaultAsync(r => r.Id.ToString() == reviewId);

        if (review != null)
        {
            return -1;
        }
        return review!.MovieId;
    }

    public async Task<int> Create(AddMovieViewModel model, string photoUrl, string videoUrl)
    {
        var movie = new Movie
        {
            Title = model.Title,
            Description = model.Description,
            ReleaseDate = model.ReleaseDate,
            Duration = model.Duration,
            CoverImageUrl = photoUrl,
            TrailerUrl = videoUrl,
            DirectorId = model.DirectorId,
        };

        await this._dbContext.Movies.AddAsync(movie);

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }

        return movie.Id;
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