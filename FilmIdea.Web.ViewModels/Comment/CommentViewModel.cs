namespace FilmIdea.Web.ViewModels.Comment;

public class CommentViewModel
{
    public string Id { get; set; } = null!;
    
    public string Content { get; set; } = null!;

    public string CommentDate { get; set; } = null!;

    public string WriterId { get; set; } = null!;

    public string WriterName { get; set; } = null!;

    public string ReviewId { get; set; } = null!;
}
