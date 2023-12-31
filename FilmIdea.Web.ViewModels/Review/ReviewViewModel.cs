﻿namespace FilmIdea.Web.ViewModels.Review;

using Comment;

public class ReviewViewModel
{
    public ReviewViewModel()
    {
        this.Comments = new HashSet<CommentViewModel>();
    }
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int Rating { get; set; }

    public string ReviewDate { get; set; } = null!;
    
    public string Content { get; set; } = null!;

    public int MovieId { get; set; }

    public int Likes { get; set; }

    public int Dislikes { get; set; }

    public string CriticId { get; set; } = null!;

    public string CriticName { get; set; } = null!;

    public AddReviewViewModel AddReview { get; set; } = null!;

    public AddCommentViewModel AddComment { get; set; } = null!;

    public ICollection<CommentViewModel> Comments { get; set; }
}
