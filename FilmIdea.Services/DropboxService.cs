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
        this._dropboxClient = new DropboxClient("sl.Bj_KVuZm616EJQSNH8dSvT1dvoF034FKtaYBbHaL8KWHZN69xRy95XG2duU3QpQyMNIQXUypn9crTX0HAa5PC6iAW6OJFTmAgt_KK-UCcAKv0cKgWirUcre01TJBHm0xK8wungO_dyrf0mI");
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
