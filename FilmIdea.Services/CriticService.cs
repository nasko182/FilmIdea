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

    public async Task<string?> GetCriticName(string userId)
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
            Name = critic.Name,
            Bio = critic.Bio,
            ProfileImageUrl = critic.ProfileImageUrl,
            DateOfBirth = critic.DateOfBirth.ToString("MMMM dd,yyyy",CultureInfo.InvariantCulture)
        };
    }

    public async Task<EditCriticViewModel> GetCriticForEditByIdAsync(string id)
    {
        var critic = await this._dbContext.Critics
            .Select(c => new EditCriticViewModel
            {
                Name = c.Name,
                Bio = c.Bio,
                DateOfBirth = c.DateOfBirth,
                ProfileImageUrl = c.ProfileImageUrl
            })
            .FirstAsync();

        return critic;
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
}
