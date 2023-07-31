namespace FilmIdea.Services.Data;

using Microsoft.EntityFrameworkCore;

using FilmIdea.Data;
using FilmIdea.Data.Models;
using FilmIdea.Data.Models.Join_Tables;
using Interfaces;
using Web.ViewModels.Chat;
using Web.ViewModels.Group;
using Web.ViewModels.Message;
using Web.ViewModels.Movie;
using Web.ViewModels.User;

public class GroupService : FilmIdeaService, IGroupService
{
    private readonly FilmIdeaDbContext _dbContext;
    public GroupService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public async Task<List<AllGroupViewModel>> GetAllGroupsAsync()
    {
        return await this._dbContext.Groups
            .Select(g => new AllGroupViewModel()
            {
                Id = g.Id,
                Name = g.Name,
                Icon = g.Icon
            })
            .ToListAsync();
    }

    public async Task CreateGroupAsync(AddGroupViewModel model)
    {
        var group = new Group()
        {
            Icon = model.Icon,
            Name = model.Name,
            Chat = new Chat()
        };

        await this._dbContext.Groups.AddAsync(group);

        if (model.UsersIds.Any())
        {
            foreach (var user in model.UsersIds)
            {
                if (user != null)
                {

                    if (this._dbContext.Users.Any(u => u.Id == Guid.Parse(user)))
                    {
                        this._dbContext.GroupsUsers.Add(new GroupUser()
                        {
                            GroupId = group.Id,
                            UserId = Guid.Parse(user)
                        });
                    }
                    else
                    {
                        throw new ArgumentException("Invalid user");
                    }
                }
            }
        }

        await this._dbContext.SaveChangesAsync();
    }

    public async Task<CreateGroupViewModel> CreateGroupModelAsync(string userId)
    {
        return new CreateGroupViewModel()
        {
            AllUsers = await this.GetAllUsersAsync(userId),
            Icons = this.GenerateIcons(),
            GroupData = new AddGroupViewModel()
        };
    }

    public async Task<DetailsGroupModel> GetGroupDetailsAsync(string groupId,string userId)
    {
        var group = await this._dbContext.Groups.FirstAsync(g => g.Id == Guid.Parse(groupId));

        var chat = await GetChatViewModel(groupId);

        var userRatings = await this._dbContext.UserRatings
            .Where(r => r.UserId.ToString() == userId)
            .Select(r => new UserRating()
            {
                Rating = r.Rating,
                MovieId = r.MovieId
            })
            .ToListAsync();

        return await this._dbContext.Groups
            .Where(g => g.Id == Guid.Parse(groupId))
            .Select(g => new DetailsGroupModel()
            {
                Icon = g.Icon,
                Name = g.Name,
                Chat = chat,
                Users = g.GroupUsers.Select(u=>new UserViewModel()
                {
                    Id = u.UserId.ToString(),
                    Username = u.User.Email.Substring(0, u.User.Email.IndexOf("@"))
                }).ToList(),
                Watchlist = g.Watchlist.Select(wm=>new MovieViewModel()
                { 
                    Id = wm.MovieId,
                    Title = wm.Movie.Title,
                    Rating = wm.Movie.CalculateUserRating(),
                    CoverPhotoUrl = wm.Movie.CoverImageUrl,
                    Duration = wm.Movie.Duration,
                    ReleaseYear = wm.Movie.ReleaseDate.Year,
                    HasMovieInWatchlist = HasMovieInUserWatchlist(userId, wm.Movie),
                    UserRating = GetRating(userRatings, wm.MovieId)
                }).ToList()
            })
            .FirstAsync();
    }




    private async Task<List<UserViewModel>> GetAllUsersAsync(string userId)
    {
        return await this._dbContext.Users
            .Where(u => u.Id != Guid.Parse(userId))
            .Select(u => new UserViewModel()
            {
                Id = u.Id.ToString(),
                Username = u.Email.Substring(0, u.Email.IndexOf("@"))
            })
            .ToListAsync();
    }

    private List<IconViewModel> GenerateIcons()
    {
        return new List<IconViewModel>()
        {
            new IconViewModel()
            {
                IconText = "🏠",
                Name = "Home"
            },
            new IconViewModel()
            {
                IconText = "💀",
                Name = "Skull"
            },
            new IconViewModel()
            {
                IconText = "👬",
                Name = "Mans"
            },
            new IconViewModel()
            {
                IconText = "👭",
                Name = "Girls"
            },
            new IconViewModel()
            {
                IconText = "❤️",
                Name = "Heart"
            },
            new IconViewModel()
            {
                IconText = "⭐",
                Name = "Star"
            },
            new IconViewModel()
            {
                IconText = "🌏",
                Name = "Globe"
            },
            new IconViewModel()
            {
                IconText = "🔥",
                Name = "Fire"
            },
            new IconViewModel()
            {
                IconText = "👀",
                Name = "Eyes"
            },
            new IconViewModel()
            {
                IconText = "😱",
                Name = "Scary"
            }
        };
    }

    private async Task<ChatViewModel> GetChatViewModel(string groupId)
    {
        var group = await this._dbContext.Groups.FirstAsync(g => g.Id == Guid.Parse(groupId));

        return new ChatViewModel()
        {
            CreatedAt = group.Chat.CreatedAt,
            Messages = group.Chat.Messages.Select(m => new MessageViewModel()
            {
                Content = m.Content,
                SendAt = m.SentAt,
                SenderId = m.SenderId.ToString(),
                SenderName = m.Sender.Email.Substring(0, m.Sender.Email.IndexOf("@"))
            }).ToList()
        };
    }

}
