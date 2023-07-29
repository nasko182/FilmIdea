namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

using Services.Data.Interfaces;
using ViewModels.Critic;

using static Common.NotificationMessageConstants;

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
            TempData[ErrorMessage] = "You are already an critic";

            return this.RedirectToAction("All", "Movie");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Become(BecomeCriticViewModel model)
    {
        if (await this._criticService.CriticExistByUserIdAsync(this.GetUserId()))
        {
            TempData[ErrorMessage] = "You are already an critic";

            return this.RedirectToAction("All", "Movie");
        }

        if (model.DateOfBirth > DateTime.UtcNow.Date)
        {
            TempData[ErrorMessage] = "Please enter an valid date of birth.";

            return this.RedirectToAction("Become");
        }

        if (model.ProfileImage == null)
        {
            TempData[ErrorMessage] = "Add profile image";

            return this.RedirectToAction("Become");
        }
        string mimeType;
        new FileExtensionContentTypeProvider().TryGetContentType(model.ProfileImage.FileName, out mimeType!);

        List<string> allowedMimeTypes = new List<string> { "image/jpeg", "image/png", "image/jp" };

        if (!allowedMimeTypes.Contains(mimeType!))
        {
            TempData[ErrorMessage] = "Inserted file is not an image";

            return this.RedirectToAction("Become");
        }

        var photoUrl = await this._criticService.UploadPhotoAsync(model.ProfileImage, this.GetUserName());

        await this._criticService.CreateCriticAsync(this.GetUserId(), model, photoUrl);

        return this.RedirectToAction("All", "Movie");
    }

}
