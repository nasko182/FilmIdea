namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using Join_Tables;

using static Common.EntityValidationConstants.GenreValidations;

public class Genre
{
    public Genre()
    {
        this.Movies = new HashSet<MovieGenre>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<MovieGenre> Movies { get; set; }
}
