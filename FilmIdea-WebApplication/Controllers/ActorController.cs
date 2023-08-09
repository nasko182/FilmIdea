﻿namespace FilmIdea.Web.Controllers;

using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Data.Interfaces;

public class ActorController : BaseController
{
    private readonly IActorService _actorService;

    public ActorController(IActorService actorService)
    {
        this._actorService = actorService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int actorId, string information)
    {
        try
        {
            var actor = await this._actorService.GetActorDetailsAsync(actorId, this.GetUserId());

            if (actor == null || actor.GetUrlInformation()!=information)
            {
                return this.NotFound();
            }
            return View(actor);
        }
        catch
        {
            return this.InvalidDataError("actor id");
        }
    }
}
