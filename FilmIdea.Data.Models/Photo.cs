namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.PhotoValidations;

public class Photo
{
    public Photo()
    {
        this.Id = Guid.NewGuid();
    }
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(UrlMaxLength)]
    public string Url { get; set; }

    public int? ActorId { get; set; }

    public Actor? Actor { get; set; }

    public int? DirectorId { get; set; }

    public Director? Director { get; set; }

    public int? MovieId { get; set; }

    public Movie? Movie { get; set; }


}
