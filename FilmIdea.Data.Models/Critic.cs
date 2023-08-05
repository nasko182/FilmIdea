 namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.CriticValidations;

public class Critic
{
    public Critic()
    {
        this.Id = Guid.NewGuid();

        this.Reviews = new HashSet<Review>();
    }
    [Key]
    public Guid Id { get; set; }

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

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; }
}