namespace FilmIdea.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Interfaces;

public class UserController : BaseAdminController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService,IDropboxService dropboxService)
    :base(dropboxService)
    {
        this._userService = userService;
    }

    [Route("User/All")]
    public async  Task<IActionResult> All()
    {
        var viewModel = await this._userService.AllAsync();

        return View( viewModel);
    }
}
