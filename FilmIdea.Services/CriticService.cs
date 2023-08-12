namespace FilmIdea.Services.Data;

using System.Globalization;
using Microsoft.EntityFrameworkCore;

using FilmIdea.Data;
using Interfaces;
using Web.ViewModels.Critic;
using FilmIdea.Data.Models;

public class CriticService : FilmIdeaService, ICriticService
{
    private readonly FilmIdeaDbContext _dbContext;

    public CriticService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;

    }
    public async Task<bool> CriticExistByUserIdAsync(string? userId)
    {
        return await this._dbContext
            .Critics
            .AnyAsync(c => c.UserId.ToString() == userId);
    }

    public async Task<string?> GetCriticIdAsync(string? userid)
    {
        Critic? critic = await this._dbContext.Critics
            .Where(c => c.UserId.ToString() == userid)
            .FirstOrDefaultAsync();

        return critic?.Id.ToString();
    }

    public async Task CreateCriticAsync(string userId, BecomeCriticViewModel model, string photoUrl)
    {

        var critic = new Critic()
        {
            Name = model.Name,
            Bio = model.Bio,
            DateOfBirth = model.DateOfBirth,
            ProfileImageUrl = photoUrl,
            UserId = Guid.Parse(userId)
        };

        await this._dbContext.Critics.AddAsync(critic);

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<string?> GetCriticNameAsync(string userId)
    {
        var critic = await this._dbContext.Critics
            .Where(c => c.UserId.ToString() == userId)
            .FirstOrDefaultAsync();

        return critic?.Name;
    }

    public async Task<CriticDetailsViewModel> GetCriticDetailsByIdAsync(string criticId)
    {
        var critic = await this._dbContext.Critics
            .FirstAsync(c => c.Id.ToString() == criticId);

        return new CriticDetailsViewModel
        {
            Id = critic.Id.ToString(),
            UseId =critic.UserId.ToString() ,
            Name = critic.Name,
            Bio = critic.Bio,
            ProfileImageUrl = critic.ProfileImageUrl,
            DateOfBirth = critic.DateOfBirth.ToString("MMMM dd,yyyy",CultureInfo.InvariantCulture)
        };
    }

    public async Task<EditCriticViewModel> GetCriticForEditByIdAsync(string id)
    {
        var critic = await this._dbContext.Critics
            .FirstAsync(c => c.Id.ToString() == id);

        var model = new EditCriticViewModel()
        {
            Name = critic.Name,
            Bio = critic.Bio,
            DateOfBirth = critic.DateOfBirth,
            ProfileImageUrl = critic.ProfileImageUrl
        };

        return model;
    }

    public async Task EditCriticByIdAndModelAsync(string criticId, EditCriticViewModel model)
    {
        var critic = await this._dbContext.Critics
            .FirstAsync(c => c.Id.ToString() == criticId);
        
        critic.Name = model.Name;
        critic.Bio = model.Bio;
        critic.DateOfBirth = model.DateOfBirth;
        critic.ProfileImageUrl = model.ProfileImageUrl;

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }

    public async Task DeleteCriticAsync(string criticId)
    {
        var critic = await this._dbContext.Critics
            .FirstAsync(c=>c.Id.ToString()==criticId);

        var user1 = this._dbContext.ApplicationUsers
            .Include(u=>u.Ratings)
            .Include(u=>u.Groups)
            .Include(u=>u.Comments)
            .Include(c=>c.Messages)
            .Include(u=>u.Likes)
            .Include(u=>u.Dislikes)
            .Include(u=>u.PassedMovies)
            .Include(c=>c.Watchlist);

           var user = await user1.FirstAsync(u => u.Id == critic.UserId);


        this._dbContext.UserRatings.RemoveRange(user.Ratings);
        this._dbContext.GroupsUsers.RemoveRange(user.Groups);
        this._dbContext.Comments.RemoveRange(user.Comments);
        this._dbContext.Messages.RemoveRange(user.Messages);
        this._dbContext.Likes.RemoveRange(user.Likes);
        this._dbContext.Dislikes.RemoveRange(user.Dislikes);
        this._dbContext.UsersMovies.RemoveRange(user.Watchlist);
        this._dbContext.PassedMovies.RemoveRange(user.PassedMovies);

        this._dbContext.Reviews.RemoveRange(critic.Reviews);

        this._dbContext.Critics.Remove(critic);
        this._dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();
    }
}
