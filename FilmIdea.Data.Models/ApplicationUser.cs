// ReSharper disable VirtualMemberCallInConstructor
namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using Join_Tables;

using static Common.EntityValidationConstants.User;

public class ApplicationUser: IdentityUser<Guid> 
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid();

        this.Ratings = new HashSet<UserRating>();
        this.Comments = new HashSet<Comment>();
        this.Groups = new HashSet<GroupUser>();
        this.Watchlist = new HashSet<UserMovie>();
        this.Messages = new HashSet<Message>();
        this.PassedMovies = new HashSet<PassedMovie>();
        this.Likes = new HashSet<Like>();
        this.DisLikes = new HashSet<Dislike>();
    }

    [Required]
    [MaxLength(UsernameMaxLength)]
    public override string UserName { get; set; } = null!;

    public Guid? CriticId { get; set; }

    public Critic? Critic { get; set; }

    public ICollection<UserRating> Ratings { get; set; }

    public ICollection<Comment> Comments { get; set; }

    public ICollection<GroupUser> Groups { get; set; }

    public ICollection<UserMovie> Watchlist { get; set; }

    public ICollection<PassedMovie> PassedMovies { get; set; }

    public ICollection<Message> Messages { get; set; }

    public ICollection<Like> Likes { get; set; }

    public ICollection<Dislike> DisLikes { get; set; }
}
