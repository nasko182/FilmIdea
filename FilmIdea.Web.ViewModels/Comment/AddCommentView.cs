namespace FilmIdea.Web.ViewModels.Comment;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.CommentValidations;

public class AddCommentViewModel
{
    [Required]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;
    
}
