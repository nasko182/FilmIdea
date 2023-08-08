namespace FilmIdea.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Interfaces;
using ViewModels.Actor;

using static Common.NotificationMessageConstants;
using static Common.ExceptionMessages;
using static Common.SuccessMessages;

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

}
