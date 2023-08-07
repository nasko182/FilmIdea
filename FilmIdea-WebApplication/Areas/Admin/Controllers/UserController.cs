namespace FilmIdea.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using Services.Data.Interfaces;
using ViewModels.User;

using static Common.GeneralApplicationConstants;

public class UserController : BaseAdminController
{
    private readonly IUserService _userService;
    private readonly IMemoryCache _memoryCache;

    public UserController(IUserService userService, IMemoryCache memoryCache, IDropboxService dropboxService)
    : base(dropboxService)
    {
        this._userService = userService;
        this._memoryCache = memoryCache;
    }

    [Route("User/All")]
    [ResponseCache(Duration = 30)]
    public async Task<IActionResult> All()
    {
        var viewModel = this._memoryCache
            .Get<IEnumerable<FullUserViewModel>>(UserCacheKey);

        if (viewModel == null)
        {
            viewModel = await this._userService.AllAsync();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(UserCacheKeyExpirationMinutes));

            this._memoryCache.Set(UserCacheKey, viewModel, cacheOptions);
        }

        return View(viewModel);
    }
}
