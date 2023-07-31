namespace FilmIdea.Web.Controllers;

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
    public async Task<IActionResult> Details(int actorId)
    {
        var actor = await this._actorService.GetActorDetailsAsync(actorId,this.GetUserId());

        return View(actor);
    }
}
