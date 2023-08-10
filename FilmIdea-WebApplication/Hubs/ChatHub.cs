namespace FilmIdea.Web.Hubs;

using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using Infrastructure.Extensions;
using Services.Data.Interfaces;
using ViewModels.Message;

using static Common.SignalRMethodsConstants;

[Authorize]
public class ChatHub : Hub
{
    private readonly IGroupService _groupService;
    private readonly IHtmlSanitizer _sanitizer;

    public ChatHub(IGroupService groupService)
    {
        this._groupService = groupService;
        this._sanitizer= new HtmlSanitizer();
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User!.GetId()!;
        var groups = await this._groupService.GetAllGroupsAsync(userId);

        foreach (var group in groups)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Id.ToString());
            
            var groupDetails = await this._groupService.GetGroupDetailsAsync(group.Id.ToString(),userId);
            var oldMessages = groupDetails.Chat.Messages;
            foreach (var message in oldMessages)
            {
                var senderName = message.SenderName;

                var sendAt = TimeZoneInfo.ConvertTimeFromUtc(message.SendAt, TimeZoneInfo.Local)
                    .ToString("dd.MM.yyyy HH:mm");
                var messageContent = $"{senderName},{message.Content},{sendAt}";

                await Clients.Group(group.Id.ToString()).SendAsync(ReceiveMessage, messageContent);
            }
        }

        await base.OnConnectedAsync();
    }

    public async Task SendMessageToGroup(MessageViewModel model,string groupId)
    {
        var senderId = Context.User!.GetId()!;

        var senderName = Context.User!.GetName()!;

        var sanitizedContent = this._sanitizer.Sanitize(model.Content);

       await this._groupService.SendMessageAsync(sanitizedContent,groupId,senderId);

        var sendAt = TimeZoneInfo.ConvertTimeFromUtc(model.SendAt, TimeZoneInfo.Local)
            .ToString("dd.MM.yyyy HH:mm");
        var messageContent = $"{senderName},{model.Content},{sendAt}";

        await Clients.Group(groupId).SendAsync(ReceiveMessage, messageContent);
    }
}
