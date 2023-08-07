namespace FilmIdea.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

using FilmIdea.Services.Data.Interfaces;

using static Common.GeneralApplicationConstants;
using static Common.NotificationMessageConstants;
using static Common.ExceptionMessages;

[Area(AdminAreaName)]
[Authorize(Roles = AdminRoleName)]
public class BaseAdminController : Controller
{
    private readonly IDropboxService _dropboxService;

    public BaseAdminController(IDropboxService dropboxService)
    {
        this._dropboxService = dropboxService;
    }
    protected IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] = GeneralErrorMessage;

        return this.RedirectToAction(this.TempData["LastAction"]!.ToString(), this.TempData["LastController"]!.ToString());
    }

    protected async Task<string> UploadPhoto(IFormFile file, string folderName, string imageName)
    {
        string mimeType;
        new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out mimeType!);

        List<string> allowedMimeTypes = new List<string> { "image/jpeg", "image/png", "image/jp" };

        if (!allowedMimeTypes.Contains(mimeType!))
        {
            throw new InvalidOperationException(InvalidInputErrorMessage("image.Inserted file is not an image"));
        }

        string photoUrl;
        try
        {
            photoUrl = await this._dropboxService.UploadFileAsync(file, folderName, imageName);
        }
        catch
        {
            throw new InvalidOperationException(InvalidToken);
        }

        return photoUrl;
    }

    protected async Task<string> UploadVideo(IFormFile file, string folderName, string videoName)
    {
        string mimeType;
        new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out mimeType);

        List<string> allowedMimeTypes = new List<string> { "video/mp4", "video/webm", "video/ogg", "video/x-msvideo", "video/quicktime", "video/mpeg", "video/3gpp", "video/x-matroska" };

        if (!allowedMimeTypes.Contains(mimeType))
        {
            throw new InvalidOperationException(InvalidInputErrorMessage("video.Inserted file is not a video"));
        }

        string videoUrl;
        try
        {
            videoUrl = await this._dropboxService.UploadFileAsync(file, folderName, videoName);
        }
        catch
        {
            return InvalidToken;
        }

        return videoUrl;
    }
}
  