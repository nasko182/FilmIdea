namespace FilmIdea.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Interfaces;
using ViewModels.Director;

using static Common.NotificationMessageConstants;
using static Common.ExceptionMessagesConstants;
using static Common.SuccessMessagesConstants;

public class DirectorController : BaseAdminController
{
    private readonly IDirectorService _directorService;

    public DirectorController(IDirectorService directorService,IDropboxService dropboxService)
     :base(dropboxService)
    {
        this._directorService = directorService;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddDirectorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return this.View(model);
        }

        var imageName = model.Name + "_ProfileImage.jpg";
        var folderName = $"ImagesDb/Directors/{model.Name}";
        string photoUrl;
        try
        {
           photoUrl = await this.UploadPhoto(model.ProfileImage, folderName, imageName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.View(model);
        }

        int directorId;
        try
        {
            directorId = await this._directorService.CreateAsync(model,photoUrl);
        }
        catch (Exception)
        {
            return this.GeneralError();
        }

        return RedirectToAction("Details", "Director", new{ Area="", directorId});
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            EditDirectorViewModel model = await this._directorService
                .GetDirectorForEditByIdAsync(id);

            return View(model);
        }
        catch
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditDirectorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await this._directorService.EditDirectorByIdAndModelAsync(id, model);
        }
        catch
        {
            ModelState.AddModelError(string.Empty, InvalidUpdate);

            return View(model);
        }
        TempData[SuccessMessage] = "Director was edited successfully!";
        return RedirectToAction("Details", "Director", new { Area = "", directorId = id });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await this._directorService.DeleteDirectorByIdAsync(id);

            TempData[SuccessMessage] = DeleteSuccess;

            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPhoto(IFormFile photo, int directorId)
    {
        var imageName = photo.FileName + "_Image.jpg";
        var folderName = $"ImagesDb/Directors/Photos/{photo.Name}";
        string photoUrl;
        try
        {
            photoUrl = await this.UploadPhoto(photo, folderName, imageName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.RedirectToAction("Details", "Director", new { Area = "", directorId });
        }

        await this._directorService.AddPhoto(directorId, photoUrl);

        return this.RedirectToAction("Details", "Director", new { Area = "", directorId });

    }

    [HttpPost]
    public async Task<IActionResult> AddVideo(IFormFile video, int directorId)
    {
        var imageName = video.FileName + "_Video.mp4";
        var folderName = $"VideosDb/Directors/Videos/{video.Name}";
        string videoUrl;
        try
        {
            videoUrl = await this.UploadVideo(video, folderName, imageName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.RedirectToAction("Details", "Director", new { Area = "", directorId });
        }

        await this._directorService.AddVideo(directorId, videoUrl);

        return this.RedirectToAction("Details", "Director", new { Area = "", directorId });

    }
}
