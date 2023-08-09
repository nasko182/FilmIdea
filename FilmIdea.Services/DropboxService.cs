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
        this._dropboxClient = new DropboxClient("sl.BjypKLbLfICY1W3fWm-KqrAIofDpsrR2kSSql4YaIZbavplxK1eDYBNTfbbI64ilTMo51eC4pB2XPXjfQrJ8byZFcidNrrD3fpqLHnH5BIb3rf_5LT5h8buB8kxZlty_YHjmjFaYJw5d4Q0");
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
