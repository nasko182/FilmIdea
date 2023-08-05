namespace FilmIdea.Web.ViewModels.User;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.User;

public class LoginFormModel
{
    [Required]
    [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
