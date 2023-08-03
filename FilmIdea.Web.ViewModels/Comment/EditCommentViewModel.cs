namespace FilmIdea.Web.ViewModels.Comment;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.CommentValidations;

public class EditCommentViewModel
{
    [Required]
    public string CommentId { get; set; } = null!;

    [Required]
    public int MovieId { get; set; }

    [Required]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;
    
}
