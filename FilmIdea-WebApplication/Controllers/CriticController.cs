namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

using Services.Data.Interfaces;
using ViewModels.Critic;

using static Common.NotificationMessageConstants;
using static Common.SuccessMessages;
using static Common.ExceptionMessages;
using FilmIdea.Web.ViewModels.Actor;

public class CriticController : BaseController
{
    private readonly ICriticService _criticService;
    private readonly IDropboxService _dropboxService;

    public CriticController(ICriticService criticService, IDropboxService dropboxService)
    {
        this._criticService = criticService;
        this._dropboxService = dropboxService;
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

        string mimeType;
        new FileExtensionContentTypeProvider().TryGetContentType(model.ProfileImage.FileName, out mimeType!);

        List<string> allowedMimeTypes = new List<string> { "image/jpeg", "image/png", "image/jp" };

        if (!allowedMimeTypes.Contains(mimeType!))
        {
            TempData[ErrorMessage] = InvalidInputErrorMessage("image.Inserted file is not an image");

            return this.RedirectToAction("Become");
        }
        var fileName = Guid.NewGuid()+this.GetUserName() + "_ProfileImage.jpg";

        string photoUrl;
        try
        {
            photoUrl = await this._dropboxService.UploadFileAsync(model.ProfileImage, "ImagesDb/Critics", fileName);
        }
        catch
        {
            TempData[ErrorMessage] = InvalidToken;
            return this.RedirectToAction("All", "Movie");
        }

        await this._criticService.CreateCriticAsync(this.GetUserId(), model, photoUrl);

        TempData[SuccessMessage] = BecomeCriticSuccess;

        return this.RedirectToAction("All", "Movie");
    }

    [HttpGet]
    public async Task<IActionResult> Details()
    {
        var isCritic =await  this._criticService.CriticExistByUserIdAsync(this.GetUserId());
        if (!isCritic)
        {
            TempData[ErrorMessage] = UnauthorizedAccessErrorMessage;

            return this.RedirectToAction("Become");
        }

        var criticId =await this._criticService.GetCriticIdAsync(this.GetUserId());

        var model =await this._criticService.GetCriticDetailsByIdAsync(criticId!);

        return this.View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var isCritic = await this._criticService.CriticExistByUserIdAsync(this.GetUserId());
        if (!isCritic)
        {
            TempData[ErrorMessage] = UnauthorizedAccessErrorMessage;

            return this.RedirectToAction("Become");
        }

        var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());
        try
        {
            EditCriticViewModel model = await this._criticService
                .GetCriticForEditByIdAsync(criticId!);

            return View(model);
        }
        catch
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditCriticViewModel model)
    {
        var isCritic = await this._criticService.CriticExistByUserIdAsync(this.GetUserId());
        if (!isCritic)
        {
            TempData[ErrorMessage] = UnauthorizedAccessErrorMessage;

            return this.RedirectToAction("Become");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());


        try
        {
            await this._criticService.EditCriticByIdAndModelAsync(criticId!, model);
        }
        catch
        {
            ModelState.AddModelError(string.Empty, InvalidUpdate);

            return View(model);
        }
        TempData[SuccessMessage] = "Critic was edited successfully!";
        return RedirectToAction("Details", "Critic", new { Area = ""});
    }

    [HttpGet]
    public async Task<IActionResult> Delete()
    {
        var isCritic = await this._criticService.CriticExistByUserIdAsync(this.GetUserId());
        if (!isCritic)
        {
            TempData[ErrorMessage] = UnauthorizedAccessErrorMessage;

            return this.RedirectToAction("Become");
        }

        var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());

        try
        {
            await this._criticService.DeleteCriticAsync(criticId!);
        }
        catch
        {
            TempData[ErrorMessage] =  InvalidUpdate;

            return this.RedirectToAction("Details");
        }

        TempData[SuccessMessage] = "Critic was deleted successfully!";

        return RedirectToAction("Logout", "User", new { Area = "" });
    }
}
