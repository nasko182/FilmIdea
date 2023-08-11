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
        this._dropboxClient = new DropboxClient("sl.Bj6uV91gvA0M6Y3gs99uwHh4MNWNGwNNLoZtWprZ38thYJHKERTJ9L42_GyVEt9nwXc5HRuMfCDIsRIHV1KWosNmLEZc3Cw0zVwou4vndrWn7PcreHNHFX7rDSISfYUlaldRjnOkZsnLb4w");
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
