namespace FilmIdea.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Interfaces;
using ViewModels.Actor;

using static Common.NotificationMessageConstants;
using static Common.ExceptionMessagesConstants;
using static Common.SuccessMessagesConstants;

public class ActorController : BaseAdminController
{
    private readonly IActorService _actorService;

    public ActorController(IActorService actorService, IDropboxService dropboxService)
    : base(dropboxService)
    {
        this._actorService = actorService;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddActorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return this.View(model);
        }

        var imageName = model.Name + "_ProfileImage.jpg";
        var folderName = $"ImagesDb/Actors/{model.Name}";
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

        int actorId;
        try
        {
            actorId = await this._actorService.CreateAsync(model,photoUrl);
        }
        catch (Exception)
        {
            return this.GeneralError();
        }

        return RedirectToAction("Details", "Actor", new{ Area="",actorId});
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            EditActorViewModel model = await this._actorService
                .GetActorForEditByIdAsync(id);

            return View(model);
        }
        catch
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id,EditActorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await this._actorService.EditActorByIdAndModelAsync(id, model);
        }
        catch 
        {
            ModelState.AddModelError(string.Empty,InvalidUpdate);

            return View(model);
        }
        TempData[SuccessMessage] = "Actor was edited successfully!";
        return RedirectToAction("Details", "Actor", new{ Area = "",actorId=id});
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await this._actorService.DeleteActorByIdAsync(id);

            TempData[SuccessMessage] = DeleteSuccess;

            return RedirectToAction("Index", "Home",new{Area="Admin"});
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditActorsForMovie(int movieId)
    {
        var model = await this._actorService.GetAllActorsAsync(movieId);

        return this.View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditActorsForMovie(int movieId, string actorIds)
    {

        var actorsIds = actorIds.Split(",").Select(id=>int.Parse(id)).ToList();
        await this._actorService.EditMovieActors(actorsIds,movieId);

        return RedirectToAction("Details", "Movie", new { Area = "", movieId });
    }

    [HttpPost]
    public async Task<IActionResult> AddPhoto(IFormFile photo,int actorId)
    {
        var imageName = photo.FileName + "_Image.jpg";
        var folderName = $"ImagesDb/Actors/Photos/{photo.Name}";
        string photoUrl;
        try
        {
            photoUrl = await this.UploadPhoto(photo, folderName, imageName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.RedirectToAction("Details", "Actor", new { Area = "", actorId });
        }

        await this._actorService.AddPhoto(actorId, photoUrl);

        return this.RedirectToAction("Details", "Actor", new { Area = "", actorId });

    }

    [HttpPost]
    public async Task<IActionResult> AddVideo(IFormFile video, int actorId)
    {
        var videoName = video.FileName + "_Video.mp4";
        var folderName = $"VideosDb/Actors/Videos/{video.Name}";
        string videoUrl;
        try
        {
            videoUrl = await this.UploadVideo(video, folderName, videoName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.RedirectToAction("Details", "Actor", new { Area = "", actorId });
        }

        await this._actorService.AddVideo(actorId, videoUrl);

        return this.RedirectToAction("Details", "Actor", new {Area="", actorId });
    }

}
