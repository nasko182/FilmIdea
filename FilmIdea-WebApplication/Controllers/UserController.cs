namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authentication;

using Griesoft.AspNetCore.ReCaptcha;

using Data.Models;
using ViewModels.User;

using static Common.NotificationMessageConstants;
using static Common.GeneralApplicationConstants;
using static Common.ExceptionMessages;

public class UserController : BaseController
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMemoryCache _memoryCache;

    public UserController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IMemoryCache memoryCache)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._memoryCache = memoryCache;
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return this.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateRecaptcha(Action = "submit",
        ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
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

        this._memoryCache.Remove(UserCacheKey);

        return this.RedirectToAction("Swipe", "Swipe");

    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl)
    {
        await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        var model = new LoginFormModel()
        {
            ReturnUrl = returnUrl
        };
        return this.View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return this.View(model);
        }

        var result = await this._signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

        if (!result.Succeeded)
        {
            TempData[ErrorMessage] = InvalidLogin;
        }

        return Redirect(model.ReturnUrl ?? "/Home/Index");
    }
}
