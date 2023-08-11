namespace FilmIdea.Web.Controllers;

using Griesoft.AspNetCore.ReCaptcha;
using Ganss.Xss;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authentication;

using Data.Models;
using ViewModels.User;

using static Common.NotificationMessageConstants;
using static Common.GeneralApplicationConstants;
using static Common.ExceptionMessagesConstants;

public class UserController : BaseController
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMemoryCache _memoryCache;
    private readonly IHtmlSanitizer _sanitizer;

    public UserController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IMemoryCache memoryCache)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._memoryCache = memoryCache;
        this._sanitizer = new HtmlSanitizer();
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
        var sanitizedModel = new RegisterFormModel
        {
            Email = this._sanitizer.Sanitize(model.Email),
            Password = this._sanitizer.Sanitize(model.Password),
            ConfirmPassword = this._sanitizer.Sanitize(model.ConfirmPassword),
            Username = this._sanitizer.Sanitize(model.Username)
        };
        if (!this.ModelState.IsValid)
        {
            return this.View(sanitizedModel);
        }
        var user = new ApplicationUser();

        await this._userManager.SetEmailAsync(user, sanitizedModel.Email);
        await this._userManager.SetUserNameAsync(user, sanitizedModel.Username);

        var result = await this._userManager.CreateAsync(user, sanitizedModel.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }

            return this.View(sanitizedModel);
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
        var sanitizedModel = new LoginFormModel()
        {
            Password = this._sanitizer.Sanitize(model.Password),
            Username = this._sanitizer.Sanitize(model.Username)
        };
        if (!ModelState.IsValid)
        {
            return this.View(sanitizedModel);
        }

        var result = await this._signInManager.PasswordSignInAsync(sanitizedModel.Username, sanitizedModel.Password, model.RememberMe, false);

        if (!result.Succeeded)
        {
            TempData[ErrorMessage] = InvalidLogin;
        }

        return Redirect(model.ReturnUrl ?? "/Home/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
