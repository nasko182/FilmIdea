namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Data.Models;
using Microsoft.AspNetCore.Authorization;
using ViewModels.User;

public class UserController : BaseController
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;

    public UserController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        this._userManager= userManager;
        this._signInManager = signInManager;
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterFormModel model)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(model);
        }
        var user = new ApplicationUser();

        await this._userManager.SetEmailAsync(user, model.Email);
        await this._userManager.SetUserNameAsync(user, model.Username);

        var result = await this._userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }

            return this.View(model);
        }

        await this._signInManager.SignInAsync(user, false);

        return this.RedirectToAction("Swipe", "Swipe");

    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login(LoginFormModel model)
    {
        return View();
    }
}
