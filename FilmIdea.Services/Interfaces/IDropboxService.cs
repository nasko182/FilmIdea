namespace FilmIdea.Services.Data.Interfaces;

using Microsoft.AspNetCore.Http;

public interface IDropboxService
{
    Task<string> UploadFileAsync(IFormFile file, string folderName,string fileName);

}
