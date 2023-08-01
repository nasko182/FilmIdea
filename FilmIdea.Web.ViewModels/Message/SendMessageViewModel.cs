namespace FilmIdea.Web.ViewModels.Message;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.MessageValidations;

public class SendMessageViewModel
{
    [Required]
    [StringLength(ContentMaxLength,MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;

    [Required]
    [StringLength(GroupIdLength, MinimumLength = GroupIdLength)]
    public string GroupId = null!;
}
