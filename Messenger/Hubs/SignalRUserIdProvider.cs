using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class SignalRUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.Identity?.Name;
    }
}