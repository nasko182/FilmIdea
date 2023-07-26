namespace FilmIdea.Web.ViewModels.Critic;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using static Common.EntityValidationConstants.CriticValidations;
public class BecomeCriticViewModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    [Display(Name = "Full name" )]
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
    public IFormFile ProfileImage { get; set;} = null!;
}

