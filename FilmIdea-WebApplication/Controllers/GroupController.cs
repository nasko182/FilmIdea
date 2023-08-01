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
        var group = await this._groupService.GetGroupDetailsAsync(groupId, this.GetUserId());

        TempData["LastGroupId"] = groupId;

        return View(group);

        // TODO: Implement receiving messages with singleR
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
                await this._groupService.CreateGroupAsync(viewModel, this.GetUserId());
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "One or more users don't exist.";
            }
        }

        return this.RedirectToAction("All");

        //TODO: Change users selection to be without CTRL or put message how users are selected
    }

    public async Task<IActionResult> Edit(string icon, string name, string userIds,string groupId)
    {
        
        List<string> userIdList = userIds.Split(",").ToList();

        var viewModel = new EditGroupViewModel
        {
            Icon = icon,
            Name = name,
            UsersIds = userIdList,
        };
        var model = await this._groupService.CreateEditGroupModelAsync(this.GetUserId(),viewModel,groupId);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditGroupViewModel viewModel,string groupId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await this._groupService.EditGroupAsync(viewModel, this.GetUserId(), groupId);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "One or more users don't exist.";
            }
        }

        return this.RedirectToAction("Details", new { groupId = TempData["LastGroupId"] });
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(string content, string groupId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await this._groupService.SendMessageAsync(content, groupId, this.GetUserId());
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Invalid Message";
            }
        }

        return this.RedirectToAction("Details", new { groupId = groupId });

        //TODO: Change users selection to be without CTRL or put message how users are selected
    }

    [HttpPost]
    public async Task<IActionResult> LeaveGroup(string groupId)
    {
        try
        {
            await this._groupService.LeaveGroupAsync(groupId, this.GetUserId());
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong.Please try again.";
        }

        return this.RedirectToAction("All");

    }
}
