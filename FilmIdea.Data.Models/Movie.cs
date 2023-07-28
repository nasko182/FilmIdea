namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using Join_Tables;

using static Common.EntityValidationConstants.MovieValidations;

public class Movie
{
    public Movie()
    {
        this.Actors = new HashSet<MovieActor>();
        this.Genres = new HashSet<MovieGenre>();
        this.Reviews = new HashSet<Review>();
        this.Ratings = new HashSet<UserRating>();
        this.GroupsWatchlists = new HashSet<GroupMovie>();
        this.UsersWatchlists = new HashSet<UserMovie>();
        this.PassedUsers = new HashSet<PassedMovie>();
        this.Photos = new HashSet<Photo>();
        this.Videos = new HashSet<Video>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    public DateTime ReleaseDate { get; set; }

    public int Duration { get; set; }

    [Required]
    [MaxLength(CoverImageUrlMaxLength)]
    public string CoverImageUrl { get; set; } = null!;

    [Required]
    [MaxLength(TrailerUrlMaxLength)]
    public string TrailerUrl { get; set; } = null!;

    public int DirectorId { get; set; }

    public Director Director { get; set; } = null!;

    public ICollection<MovieActor> Actors { get; set; }

    public ICollection<MovieGenre> Genres { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<UserRating> Ratings { get; set; }

    public ICollection<GroupMovie> GroupsWatchlists { get; set; }

    public ICollection<UserMovie> UsersWatchlists { get; set; }

    public ICollection<PassedMovie> PassedUsers { get; set; }

    public ICollection<Photo> Photos { get; set; }

    public ICollection<Video> Videos { get; set; }


    public decimal CalculateUserRating()
    {
        if (this.Ratings.Any())
        {
            return (decimal)this.Ratings.Average(r => r.Rating);
        }
        else
        {
            return 0.0m;
        }
    }
}