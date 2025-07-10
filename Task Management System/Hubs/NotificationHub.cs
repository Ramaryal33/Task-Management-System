using Microsoft.AspNetCore.SignalR;

namespace TaskManagementSystem.Web.Hubs;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var user = Context.GetHttpContext()?.Request.Query["user"].ToString();
        if (!string.IsNullOrWhiteSpace(user))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, user);
        }
        await base.OnConnectedAsync();
    }
}
