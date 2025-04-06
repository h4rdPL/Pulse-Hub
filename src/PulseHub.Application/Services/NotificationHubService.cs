using Microsoft.AspNetCore.SignalR;
using PulseHub.Application.Hubs;
using PulseHub.Core.Interfaces;

public class NotificationHubService : INotificationHub
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationHubService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(string userEmail, string message)
    {
        await _hubContext.Clients.User(userEmail).SendAsync("ReceiveNotification", message);
    }
}
