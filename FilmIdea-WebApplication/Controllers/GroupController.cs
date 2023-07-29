namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Data.Interfaces;
using ViewModels.Group;

using static Common.NotificationMessageConstants;

public class GroupController : BaseController
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        this._groupService = groupService;
    }
    public async Task<IActionResult> All()
    {
        var groups = await this._groupService.GetAllGroupsAsync();

        return View(groups);
    }

    public async Task<IActionResult> Details(string groupId)
    {
        var group = await this._groupService.GetGroupDetailsAsync(groupId);

        return View(group);
    }

    public async Task<IActionResult> Create()
    {
        var model = await this._groupService.CreateGroupModelAsync(this.GetUserId());

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddGroupViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await this._groupService.CreateGroupAsync(viewModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "One or more users don't exist.";
            }
        }

        return this.RedirectToAction("All");

        //TODO: Change users selection to be without CTRL ore put message how users are selected
    }
}
