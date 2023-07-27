namespace FilmIdea.Services.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using FilmIdea.Data;
using Interfaces;
using Web.ViewModels.Critic;
using Dropbox.Api.Files;
using Dropbox.Api;
using FilmIdea.Data.Models;

public class CriticService : ICriticService
{
    private readonly FilmIdeaDbContext _dbContext;

    private DropboxClient _dropboxClient;

    public CriticService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;

        this._dropboxClient = new DropboxClient("sl.Bi-TRNmAfLQStYqDFmO79g6VCRRtMmI5-j1XVP8s27QddBXQcUe6BZTZW9Vf46ce5so4Bh4fgiA7ilWx0l17azIcAB8dg3i6-IAlKzU6-E3IACPmG0CVeN4TyKoN-yCmn9BwpaFcCC1TgIQ");
    }
    public async Task<bool> CriticExistByUserIdAsync(string userId)
    {
        return await this._dbContext
            .Critics
            .AnyAsync(c => c.UserId.ToString() == userId);
    }

    public async Task<string> UploadPhotoAsync(IFormFile imageFile,string userName)
    {
        var fileName = userName + "_ProfileImage";
        var path = "/Critics/" + fileName;

        try
        {
            await using (var stream = imageFile.OpenReadStream())
            {
                var uploadResult = await _dropboxClient.Files.UploadAsync(path, WriteMode.Overwrite.Instance, body: stream);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var sharedLink = await _dropboxClient.Sharing.CreateSharedLinkWithSettingsAsync(path);

        var imageUrl = sharedLink.Url;

        Uri dropboxUri = new Uri(imageUrl);

        string baseUrl = dropboxUri.GetLeftPart(UriPartial.Path);

        string modifiedUrl = baseUrl.Replace("www.dropbox.com", "dl.dropboxusercontent.com");

        modifiedUrl = modifiedUrl.Split('?')[0];

        return modifiedUrl;
    }

    public async Task CreateCriticAsync(string userId, BecomeCriticViewModel model,string photoUrl)
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
}
