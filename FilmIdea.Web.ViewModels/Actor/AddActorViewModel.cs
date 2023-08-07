namespace FilmIdea.Web.ViewModels.Actor;

using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.ActorValidations;

public class AddActorViewModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    [Display(Name = "Full Name")]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(BioMaxLength, MinimumLength = BioMinLength)]
    [Display(Name = "Biography")]
    public string Bio { get; set; } = null!;

    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Profile Image")]
    public IFormFile ProfileImage { get; set; } = null!;
}
