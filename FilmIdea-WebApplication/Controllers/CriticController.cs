namespace FilmIdea.Web.Controllers;

using Ganss.Xss;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

using Services.Data.Interfaces;
using ViewModels.Critic;

using static Common.NotificationMessageConstants;
using static Common.SuccessMessagesConstants;
using static Common.ExceptionMessagesConstants;

public class CriticController : BaseController
{
    private readonly ICriticService _criticService;
    private readonly IDropboxService _dropboxService;
    private readonly IHtmlSanitizer _sanitizer;


    public CriticController(ICriticService criticService, IDropboxService dropboxService)
    {
        this._criticService = criticService;
        this._dropboxService = dropboxService;
        this._sanitizer = new HtmlSanitizer();
    }

    [HttpGet]
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

        var sanitizedModel = new BecomeCriticViewModel
        {
            Name = this._sanitizer.Sanitize(model.Name),
            Bio = this._sanitizer.Sanitize(model.Bio),
            DateOfBirth = model.DateOfBirth,
            ProfileImage = model.ProfileImage
        };

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

        await this._criticService.CreateCriticAsync(this.GetUserId(), sanitizedModel, photoUrl);

        TempData[SuccessMessage] = BecomeCriticSuccess;

        return this.RedirectToAction("All", "Movie");
    }

    [HttpGet]
    public async Task<IActionResult> Details(string criticId)
    {
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
            var sanitizedModel = new EditCriticViewModel
            {
                Name = this._sanitizer.Sanitize(model.Name),
                Bio = this._sanitizer.Sanitize(model.Name),
                DateOfBirth = model.DateOfBirth,
                ProfileImageUrl = model.ProfileImageUrl
            };
            return View(sanitizedModel);
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
        var sanitizedModel = new EditCriticViewModel
        {
            Name = this._sanitizer.Sanitize(model.Name),
            Bio = this._sanitizer.Sanitize(model.Name),
            DateOfBirth = model.DateOfBirth,
            ProfileImageUrl = model.ProfileImageUrl
        };

        if (!ModelState.IsValid)
        {
            return View(sanitizedModel);
        }

        var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());


        try
        {
            await this._criticService.EditCriticByIdAndModelAsync(criticId!, sanitizedModel);
        }
        catch
        {
            ModelState.AddModelError(string.Empty, InvalidUpdate);

            return View(sanitizedModel);
        }
        TempData[SuccessMessage] = "Critic was edited successfully!";
        return RedirectToAction("Details", "Critic", new { Area = "",criticId});
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

        TempData[SuccessMessage] = DeleteCriticSuccess;

        return RedirectToAction("Logout", "User", new { Area = "" });
    }
}
