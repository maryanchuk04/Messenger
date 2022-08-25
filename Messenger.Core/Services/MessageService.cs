using System.Reflection;
using Messenger.Core.IServices;
using Messenger.db.Bridge;
using Messenger.db.EF;
using Messenger.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Core.Services;

public class MessageService : BaseService<Message>, IMessageService
{
    private readonly ISecurityContext _securityContext;

    public MessageService(MessengerContext context, ISecurityContext securityContext) : base(context)
    {
        _securityContext = securityContext;
    }


    public async Task<Message> Send(Guid chatId,Guid sender, string text)
    {
        var chat = Context.Rooms.Find(chatId);
        if (chat == null)
        {
            chat = Context.Rooms
                .First(x => x.Users.Count == 2 && x.Users.Any(y => y.UserId == chatId) && x.Users.Any(y => y.UserId == sender));
        }

        var msg = Insert(new Message { RoomId = chat.Id, SenderId = sender, Content = text });
        await Context.SaveChangesAsync();

        return msg;
    }

    public async Task<Room> GetRoom(Guid chatId, Guid sender)
    {
        var chat = Context.Rooms
                       .FirstOrDefault(x => x.Id == chatId) ??
                   Context.Rooms
                       .FirstOrDefault(x =>
                           x.Users.Count == 2 &&
                           x.Users.Any(y => y.UserId == chatId) &&
                           x.Users.Any(y => y.UserId == sender));

        if (chat == null)
        {
            chat = new Room()
            {
                Name = Context.Users.FirstOrDefault(x=>x.Id == chatId).UserName
            };
            chat.Users = new List<UserRoom>
            {
                new UserRoom { Room = chat, UserId = sender },
                new UserRoom { Room = chat, UserId = chatId },
            };
            Context.Rooms.Add(chat);
            await Context.SaveChangesAsync();
        }

        var res = Context.Rooms
            .Include(c => c.Messages.OrderByDescending(x=>x.When).Take(20))
                .ThenInclude(x=>x.Sender)
            .FirstOrDefault(x => x.Id == chat.Id);


        return res;

    }



    public  List<Room> GetUsersRooms(Guid userId)
    {
        var rooms = Context.Rooms
            .Include(x => x.Users)
                .ThenInclude(x => x.User)
            .Include(m => m.Messages)
            .Where(x => x.Users.Any(x => x.UserId == userId));

        return rooms.ToList();
    }



    public async Task<Room> CreateChatRoom(Guid currentUserId, Guid otherUserId)
    {
        if (Context.Rooms.Any(x => x.Users.Any(x => x.UserId == currentUserId && x.UserId == otherUserId)))
        {
            throw new Exception("Chat is already exist");
        }
        var chat = new Room();
        chat.Users = new List<UserRoom>()
        {
            new UserRoom() {Room = chat, UserId = currentUserId},
            new UserRoom() {Room = chat, UserId = otherUserId}
        };

        Context.Rooms.Add(chat);
        await Context.SaveChangesAsync();

        return chat;
    }

    public List<string> GetChatUserIds(Guid chatId)
    {
        return Context.Rooms
            .Include(c => c.Users)
            .First(x => x.Id == chatId).Users.Select(y => y.UserId.ToString()).ToList();
    }

    public List<Room> Search(string keyword)
    {
        var messenges = Context.Messages
            .Include(x => x.Sender)
            .Where(x=>x.Content.Contains(keyword)).ToList();

        if (messenges.Count == 0)
        {
            return null;
        }

        var rooms = new List<Room>();

        foreach (var item in messenges)
        {
            rooms.Add(Context.Rooms.Include(x=>x.Messages).FirstOrDefault(x=>x.Messages.Contains(item)));
        }

        return rooms;
    }
}