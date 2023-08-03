namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Data.Interfaces;
using ViewModels.Group;

using static Common.NotificationMessageConstants;
using static Common.ExceptionMessages;

public class GroupController : BaseController
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        this._groupService = groupService;
    }
    public async Task<IActionResult> All()
    {
        try
        {
            var groups = await this._groupService.GetAllGroupsAsync();

            return View(groups);
        }
        catch
        {
            return this.GeneralError();
        }
    }

    public async Task<IActionResult> Details(string groupId)
    {
        try
        {
            var group = await this._groupService.GetGroupDetailsAsync(groupId, this.GetUserId());

            TempData["LastGroupId"] = groupId;

            return View(group);
        }
        catch
        {
            return this.InvalidDataError("genre id");
        }
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            var model = await this._groupService.CreateGroupModelAsync(this.GetUserId());

            return View(model);
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddGroupViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
               var groupId =  await this._groupService.CreateGroupAsync(viewModel, this.GetUserId());

                return RedirectToAction("Details",new{groupId});
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = UsersDontExistErrorMessage;
            }
        }

        return this.RedirectToAction("All");
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
                TempData[ErrorMessage] = UsersDontExistErrorMessage;
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
                TempData[ErrorMessage] = InvalidInputErrorMessage("message");
            }
        }

        return this.RedirectToAction("Details", new { groupId });
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
            TempData[ErrorMessage] = GeneralErrorMessage;
        }

        return this.RedirectToAction("All");

    }
}
