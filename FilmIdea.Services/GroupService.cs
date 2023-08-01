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
using static Dropbox.Api.Files.SearchMatchType;

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

    public async Task CreateGroupAsync(AddGroupViewModel model, string userId)
    {
        var chat = new Chat();
        var group = new Group()
        {
            Icon = model.Icon,
            Name = model.Name,
            Chat = chat,
            ChatId = chat.Id
        };

        await this._dbContext.Groups.AddAsync(group);

        if (model.UsersIds.Any())
        {
            model.UsersIds.Add(userId);
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
            AddGroupData = new AddGroupViewModel()
        };
    }

    public async Task EditGroupAsync(EditGroupViewModel model, string userId, string groupId)
    {
        var group = await _dbContext.Groups
            .Include(e => e.GroupUsers) 
            .SingleOrDefaultAsync(e => e.Id.ToString() == groupId);


        if (group == null)
        {
            // Handle the case where the entity is not found
            return;
        }

        group.Name = model.Name;
        group.Icon = model.Icon;

        var usersToRemove = group.GroupUsers
            .Where(u => !model.UsersIds.Contains(u.UserId.ToString()))
            .ToList();

        var existingUserIds = group.GroupUsers.Select(u => u.UserId.ToString()).ToList();
        var newUserIdsToAdd = model.UsersIds
            .Where(id=>id.Length==36)
            .Except(existingUserIds).ToList();

        foreach (var id in newUserIdsToAdd)
        {
            var newUser = new GroupUser()
            {
                UserId = Guid.Parse(id),
                GroupId = Guid.Parse(groupId)
            };
            group.GroupUsers.Add(newUser);
        }

        _dbContext.GroupsUsers.RemoveRange(usersToRemove);


        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public async Task<CreateEditGroupViewModel> CreateEditGroupModelAsync(string userId, EditGroupViewModel model, string groupId)
    {
        return new CreateEditGroupViewModel()
        {
            Id = groupId,
            AllUsers = await this.GetAllUsersAsync(userId),
            Icons = this.GenerateIcons(),
            GroupData = model
        };
    }

    public async Task<DetailsGroupModel> GetGroupDetailsAsync(string groupId, string userId)
    {
        var chat = await GetChatViewModel(groupId);

        var userRatings = await this._dbContext.UserRatings
            .Where(r => r.UserId.ToString() == userId)
            .Select(r => new UserRating()
            {
                Rating = r.Rating,
                MovieId = r.MovieId
            })
            .ToListAsync();

        var groupMovies = await GetGroupMoviesAsync(groupId);

        return await this._dbContext.Groups
            .Include(g => g.GroupUsers)
            .Where(g => g.Id == Guid.Parse(groupId))
            .Select(g => new DetailsGroupModel()
            {
                Id = groupId,
                Icon = g.Icon,
                Name = g.Name,
                Chat = chat,
                Users = g.GroupUsers.Select(u => new UserViewModel()
                {
                    Id = u.UserId.ToString(),
                    Username = u.User.Email.Substring(0, u.User.Email.IndexOf("@"))
                }).ToList(),
                Watchlist = groupMovies.Select(m => new MovieViewModel()
                {
                    CoverPhotoUrl = m.CoverImageUrl,
                    Duration = m.Duration,
                    Id = m.Id,
                    ReleaseYear = m.ReleaseDate.Year,
                    Title = m.Title,
                    Rating = m.CalculateUserRating(),
                    UserRating = GetRating(userRatings, m.Id),
                    HasMovieInWatchlist = HasMovieInUserWatchlist(userId, m),
                }).ToList()
            })
            .FirstAsync();
    }

    public async Task SendMessageAsync(string content, string groupId, string userId)
    {
        var chat = await this._dbContext.Chats
            .Where(c => c.GroupId == Guid.Parse(groupId))
            .FirstOrDefaultAsync();
        if (chat == null)
        {
            throw new InvalidOperationException("Invalid group");
        }

        await this._dbContext.Messages.AddAsync(new Message()
        {
            Content = content,
            SenderId = Guid.Parse(userId),
            Chat = chat
        });

        await this._dbContext.SaveChangesAsync();
    }

    public async Task LeaveGroupAsync(string groupId, string userId)
    {
        var group = await this._dbContext.Groups
            .Where(g => g.Id.ToString() == groupId)
            .Include(g => g.GroupUsers)
            .FirstOrDefaultAsync();
        if (group == null)
        {
            throw new InvalidOperationException("Invalid group");
        }
        var groupUser = group.GroupUsers
            .FirstOrDefault(gu => gu.UserId.ToString() == userId && gu.GroupId.ToString() == groupId);
        if (groupUser == null)
        {
            throw new InvalidOperationException("Invalid group");
        }
        group.GroupUsers.Remove(groupUser);
        await this._dbContext.SaveChangesAsync();
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
        var group = await this._dbContext.Groups
            .Include(g => g.Chat)
            .ThenInclude(c => c.Messages)
            .ThenInclude(m => m.Sender)
            .FirstAsync(g => g.Id == Guid.Parse(groupId));

        return new ChatViewModel()
        {
            CreatedAt = group.Chat.CreatedAt,
            Messages = group.Chat.Messages
                .OrderBy(m => m.SentAt)
                .Select(m => new MessageViewModel()
                {
                    Content = m.Content,
                    SendAt = m.SentAt,
                    SenderId = m.SenderId.ToString(),
                    SenderName = m.Sender.Email.Substring(0, m.Sender.Email.IndexOf("@"))
                }).ToList()
        };
    }

    private async Task<List<Movie>> GetGroupMoviesAsync(string groupId)
    {
        var group = await this._dbContext.Groups
            .Include(g => g.GroupUsers)
            .ThenInclude(gm => gm.User)
            .ThenInclude(u => u.Watchlist)
            .ThenInclude(um => um.Movie)
            .ThenInclude(m => m.Ratings)
            .Where(g => g.Id == Guid.Parse(groupId)).FirstAsync();

        if (group.GroupUsers == null || !group.GroupUsers.Any())
        {
            return new List<Movie>();

        }

        var firstUserMovies = group.GroupUsers.First().User.Watchlist.Select(um => um.Movie).ToList();
        var sharedMovies = firstUserMovies;

        foreach (var userGroup in group.GroupUsers.Skip(1))
        {
            var currentUserMovies = userGroup.User.Watchlist
                .Select(um => um.Movie).ToList();
            sharedMovies = sharedMovies.Intersect(currentUserMovies).ToList();
        }

        return sharedMovies;
    }



}
