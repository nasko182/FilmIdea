namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.DirectorValidations;
public class Director
{
    public Director()
    {
        this.Movies = new HashSet<Movie>();
        this.Photos = new HashSet<Photo>();
        this.Videos = new HashSet<Video>();
    }
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(BioMaxLength)]
    public string Bio { get; set; } = null!;

    [Required]
    [MaxLength(ProfileImageUrlMaxLength)]
    public string ProfileImageUrl { get; set; } = null!;

    [Required]
    public DateTime DateOfBirth { get; set; }

    public ICollection<Movie> Movies { get; set; }

    public ICollection<Video> Videos { get; set; }

    public ICollection<Photo> Photos { get; set; }
}