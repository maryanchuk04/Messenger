using Messenger.Core.IServices;
using Messenger.db.Bridge;
using Messenger.db.EF;
using Messenger.db.Entities;
using Messenger.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class ChatRoom : Hub
{

    private readonly ISecurityContext _securityContext;
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;
    private readonly IDictionary<string, string> _connections = new Dictionary<string, string>();

    public ChatRoom(IMessageService messageService, IUserService userService,ISecurityContext securityContext)
    {
        _messageService = messageService;
        _userService = userService;
        _securityContext = securityContext;
    }

    public async Task Send(Guid chatId, string message)
    {
        message = message.Trim();
        if (message != string.Empty)
        {
            var currentUserId = _securityContext.GetCurrentUserId();
            var res = await _messageService.Send(chatId, currentUserId, message);

            await Clients.Group(chatId.ToString()).SendAsync("RecieveMessage", res);
            await Clients.Group(chatId.ToString()).SendAsync("ShowAlert", res);
        }
    }

    public async Task JoinRoom(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        _connections[Context.ConnectionId] = chatId;
        Console.WriteLine("WORK");
        await Clients.Group(chatId).SendAsync("JoinToRoom", "Was connected to room");
        await SendUsersConnected(chatId);
    }

    public async Task JoinToUsersRooms(List<string> chatsIds)
    {
        foreach (var item in chatsIds)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, item);
            _connections[Context.ConnectionId] = item;
            Console.WriteLine("WORK");
            await Clients.Group(item).SendAsync("JoinToUsersRooms", "Was connected to room");
            await SendUsersConnected(item);
        }
    }


    public Task SendUsersConnected(string chatId)
    {
        var users = _connections.Values.Where(x => x == chatId);

        return Clients.Group(chatId).SendAsync("UsersInRoom", users);
    }

    public async Task Typing(string chatId, string userId)
    {
        Console.WriteLine("Is typing...");
        await Clients.Group(chatId).SendAsync("TypingMessage", userId);
    }
}