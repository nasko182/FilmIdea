namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

using Services.Data.Interfaces;
using ViewModels.Critic;

using static Common.NotificationMessageConstants;
using static Common.SuccessMessages;
using static Common.ExceptionMessages;

public class CriticController : BaseController
{
    private readonly ICriticService _criticService;

    public CriticController(ICriticService criticService)
    {
        this._criticService = criticService;
    }
    public async Task<IActionResult> Become()
    {
        if (await this._criticService.CriticExistByUserIdAsync(this.GetUserId()))
        {
            TempData[ErrorMessage] = AlreadyCriticErrorMessage;

            return this.RedirectToAction("All", "Movie");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Become(BecomeCriticViewModel model)
    {
        if (await this._criticService.CriticExistByUserIdAsync(this.GetUserId()))
        {
            TempData[ErrorMessage] = AlreadyCriticErrorMessage;

            return this.RedirectToAction("All", "Movie");
        }

        if (model.DateOfBirth > DateTime.UtcNow.Date || model.DateOfBirth.Year < 1900)
        {
            TempData[ErrorMessage] = InvalidDateOfBirthErrorMessage;

            return this.RedirectToAction("Become");
        }

        if (model.ProfileImage == null)
        {
            TempData[ErrorMessage] = InvalidInputErrorMessage("picture");

            return this.RedirectToAction("Become");
        }
        string mimeType;
        new FileExtensionContentTypeProvider().TryGetContentType(model.ProfileImage.FileName, out mimeType!);

        List<string> allowedMimeTypes = new List<string> { "image/jpeg", "image/png", "image/jp" };

        if (!allowedMimeTypes.Contains(mimeType!))
        {
            TempData[ErrorMessage] = InvalidInputErrorMessage("image.Inserted file is not an image");

            return this.RedirectToAction("Become");
        }

        string photoUrl = String.Empty;
        try
        {
            photoUrl = await this._criticService.UploadPhotoAsync(model.ProfileImage, this.GetUserName());
        }
        catch
        {
            TempData[ErrorMessage] = InvalidToken;
        }

        await this._criticService.CreateCriticAsync(this.GetUserId(), model, photoUrl);

        TempData[SuccessMessage] = BecomeCriticSuccess;

        return this.RedirectToAction("All", "Movie");
    }

}
