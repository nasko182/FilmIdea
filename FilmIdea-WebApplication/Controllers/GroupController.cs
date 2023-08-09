namespace FilmIdea.Web.Controllers;

using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Services.Data.Interfaces;
using ViewModels.Group;

using static Common.NotificationMessageConstants;
using static Common.ExceptionMessages;

public class GroupController : BaseController
{
    private readonly IGroupService _groupService;
    private readonly IHtmlSanitizer _sanitizer;

    public GroupController(IGroupService groupService)
    {
        this._groupService = groupService;
        this._sanitizer = new HtmlSanitizer();
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
        var sanitizedModel = new AddGroupViewModel
        {
            Name = this._sanitizer.Sanitize(viewModel.Name),
            Icon = this._sanitizer.Sanitize(viewModel.Icon),
            UsersIds = viewModel.UsersIds.Select(uId => this._sanitizer.Sanitize(uId)).ToArray()
        };
        if (ModelState.IsValid)
        {
            try
            {
                var groupId = await this._groupService.CreateGroupAsync(sanitizedModel, this.GetUserId());

                return RedirectToAction("Details", new { groupId });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = UsersDoNotExistErrorMessage;
            }
        }

        var model = await this._groupService.CreateGroupModelAsync(this.GetUserId());
        model.AddGroupData = sanitizedModel;

        return this.View(model);
    }

    public async Task<IActionResult> Edit(string icon, string name, string userIds, string groupId)
    {
        List<string> userIdList = userIds.Split(",").ToList();

        var sanitizedViewModel = new EditGroupViewModel
        {
            Name = this._sanitizer.Sanitize(name),
            Icon = this._sanitizer.Sanitize(icon),
            UsersIds = userIdList.Select(uId => this._sanitizer.Sanitize(uId)).ToArray()
        };
        var model = await this._groupService.CreateEditGroupModelAsync(this.GetUserId(), sanitizedViewModel, groupId);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditGroupViewModel viewModel, string groupId)
    {
        var sanitizedModel = new EditGroupViewModel()
        {
            Name = this._sanitizer.Sanitize(viewModel.Name),
            Icon = this._sanitizer.Sanitize(viewModel.Icon),
            UsersIds = viewModel.UsersIds.Select(uId => this._sanitizer.Sanitize(uId)).ToArray()
        };
        if (ModelState.IsValid)
        {
            try
            {
                await this._groupService.EditGroupAsync(sanitizedModel, this.GetUserId(), groupId);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = UsersDoNotExistErrorMessage;
            }
        }

        return this.RedirectToAction("Details", new { groupId = TempData["LastGroupId"] });
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(string content, string groupId)
    {
        var sanitizedContent = this._sanitizer.Sanitize(content);
        if (ModelState.IsValid)
        {
            try
            {
                await this._groupService.SendMessageAsync(sanitizedContent, groupId, this.GetUserId());
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
