﻿namespace FilmIdea.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Interfaces;
using ViewModels.Movie;

using static Common.NotificationMessageConstants;
using static Common.ExceptionMessagesConstants;
using static Common.SuccessMessagesConstants;

public class MovieController : BaseAdminController
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService, IDropboxService dropboxService)
     : base(dropboxService)
    {
        this._movieService = movieService;
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddMovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return this.View(model);
        }

        var imageName = model.Title + "_CoverImage.jpg";
        var folderName = $"ImagesDb/Movies/{model.Title}";
        string photoUrl;
        try
        {
            photoUrl = await this.UploadPhoto(model.CoverImage, folderName, imageName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.View(model);
        }

        var videoName = model.Title + "_Trailer.mp4";
        folderName = $"VideosDb/Movies/{model.Title}";
        string videoUrl;
        try
        {
            videoUrl = await this.UploadVideo(model.Trailer, folderName, videoName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.View(model);
        }

        int movieId;
        try
        {
            movieId = await this._movieService.CreateAsync(model, photoUrl,videoUrl);
        }
        catch (Exception)
        {
            return this.GeneralError();
        }

        return RedirectToAction("Details", "Movie", new { Area = "", movieId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            EditMovieViewModel model = await this._movieService
               .GetMovieForEditByIdAsync(id);

            return View(model);
        }
        catch
        {
            return GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditMovieViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await this._movieService.EditMovieByIdAndModelAsync(id, model);
        }
        catch
        {
            ModelState.AddModelError(string.Empty, InvalidUpdate);

            return View(model);
        }
        TempData[SuccessMessage] = "Movie was edited successfully!";

        return RedirectToAction("Details", "Movie", new { Area = "", movieId = id });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await this._movieService.DeleteMovieByIdAsync(id);

            TempData[SuccessMessage] = DeleteSuccess;

            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
        catch (Exception)
        {
            return GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditGenresForMovie(int movieId)
    {
        var model = await this._movieService.GetAllGenresAsync(movieId);

        return this.View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditGenresForMovie(int movieId, string genresIds)
    {

        var genresIdsList = genresIds.Split(",").Select(id => int.Parse(id)).ToList();
        await this._movieService.EditMovieGenresAsync(genresIdsList, movieId);

        return RedirectToAction("Details", "Movie", new { Area = "", movieId });
    }

    [HttpPost]
    public async Task<IActionResult> AddPhoto(IFormFile photo, int movieId)
    {
        var imageName = photo.FileName + "_Image.jpg";
        var folderName = $"ImagesDb/Movies/Photos/{photo.Name}";
        string photoUrl;
        try
        {
            photoUrl = await this.UploadPhoto(photo, folderName, imageName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.RedirectToAction("Details", "Movie", new { Area = "", movieId });
        }

        await this._movieService.AddPhotoAsync(movieId, photoUrl);

        return this.RedirectToAction("Details", "Movie", new { Area = "", movieId });

    }

    [HttpPost]
    public async Task<IActionResult> AddVideo(IFormFile video, int movieId)
    {
        var imageName = video.FileName + "_Video.mp4";
        var folderName = $"VideosDb/Movies/Videos/{video.Name}";
        string videoUrl;
        try
        {
            videoUrl = await this.UploadVideo(video, folderName, imageName);
        }
        catch (Exception e)
        {
            TempData[ErrorMessage] = e.Message;

            return this.RedirectToAction("Details", "Movie", new { Area = "", movieId });
        }

        await this._movieService.AddVideoAsync(movieId, videoUrl);

        return this.RedirectToAction("Details", "Movie", new { Area = "", movieId });

    }
}
