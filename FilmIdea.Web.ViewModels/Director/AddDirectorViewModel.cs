namespace FilmIdea.Web.ViewModels.Director;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using static Common.EntityValidationConstants.DirectorValidations;

public class AddDirectorViewModel
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
