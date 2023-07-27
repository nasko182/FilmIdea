namespace FilmIdea.Services.Data;

//using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Interfaces;
using FilmIdea.Data;
using FilmIdea.Data.Models;
using FilmIdea.Data.Models.Join_Tables;
using Web.ViewModels.Actor;
using Web.ViewModels.Director;
using Web.ViewModels.Movie;

public class FilmIdeaService : IFilmIdeaService
{
    private readonly FilmIdeaDbContext _dbContext;
    private readonly Random _random;

    public FilmIdeaService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
        this._random = new Random();
    }

    public async Task<AllMoviesViewModel> GetAllMoviesAsync(string? userId)
    {
        var n = await this.GetMoviesForTopSectionAsync();
        return new AllMoviesViewModel()
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

    public async Task<MovieDetailsViewModel> GetMovieAsync(int movieId, string userId)
    {
        var userRating = await this._dbContext.UserRatings
            .Where(r => r.UserId.ToString() == userId)
            .Select(r => r.Rating)
            .FirstOrDefaultAsync();

        var movie = await this._dbContext
            .Movies
            .Where(m => m.Id == movieId)
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

                // Populate other related data properties
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
                Genres = m.Genres.Select(mg => mg.Genre).ToList(),
                Reviews = m.Reviews,
                Photos = m.Photos,
                Videos = m.Videos,
                UserRating = userRating
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
                })
                .FirstAsync();

            return movie;
        }
    }

    public async Task AddRatingAsync(int movieId, int ratingValue, string userId)
    {
        var movie = await this._dbContext.Movies
            .Include(m => m.Ratings)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie != null)
        {
            var userRating = movie.Ratings.FirstOrDefault(ur => ur.UserId.ToString() == userId);

            if (userRating != null)
            {
                userRating.Rating = ratingValue;
            }
            else
            {
                movie.Ratings.Add(new UserRating()
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
                    ReleaseYear = m.ReleaseDate.Year,
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
                    UserRating = GetRating(userRatings, m.Id)
                })
                .ToListAsync();

            return movies;
        }
    }

    private async Task<IEnumerable<TopSectionMovieViewModel>> GetMoviesForTopSectionAsync()
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

    private static int GetRating(ICollection<UserRating> userRating, int movieId)
    {
        if (userRating.Any())
        {
            var movieRating = userRating.FirstOrDefault(ur => ur.MovieId == movieId);
            if (movieRating != null)
            {
                return movieRating.Rating;
            }
        }
        return 0;

    }

    private static bool HasMovieInUserWatchlist(string userId, Movie movie)
    {
        return movie.UsersWatchlists.Any(um => um.MovieId == movie.Id && um.UserId == Guid.Parse(userId));
    }
}