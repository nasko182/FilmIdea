namespace FilmIdea.Services.Data;

using Microsoft.AspNetCore.Http;

using Dropbox.Api.Files;
using Dropbox.Api;

using Interfaces;

public class DropboxService : IDropboxService
{

    private DropboxClient _dropboxClient;

    public DropboxService()
    {
        this._dropboxClient = new DropboxClient("sl.Bj1UpKbmkFfJq2hp50tptKJU33UWSpBMR58BBp-hOimK4iu_uxqp25aAZ-C7r87yfRiLGMhk48hqbPmBW788BMuX_12DOC86UyLipfTjS0iZug3gwIFof0e_d9gmYLOoZocjafqLjb7XdC0");
    }
    public async Task<string> UploadFileAsync(IFormFile file,string folderName,string fileName)
    {
        var path = $"/{folderName}/{fileName}";

        try
        {
            await using (var stream = file.OpenReadStream())
            {
                await _dropboxClient.Files.UploadAsync(path, WriteMode.Overwrite.Instance, body: stream);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var sharedLink = await _dropboxClient.Sharing.CreateSharedLinkWithSettingsAsync(path);

        var fileUrl = sharedLink.Url;

        var dropboxUri = new Uri(fileUrl);

        var baseUrl = dropboxUri.AbsoluteUri;

        var modifiedUrl = baseUrl.Replace("www.dropbox.com", "dl.dropboxusercontent.com");

        var separator = '?';
        if(baseUrl.Contains('&'))
        {
            separator = '&';
        }

        modifiedUrl = modifiedUrl.Split(separator)[0];

        return modifiedUrl;
    }
}
