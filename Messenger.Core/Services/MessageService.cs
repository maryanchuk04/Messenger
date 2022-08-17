using System.Reflection;
using Messenger.Core.IServices;
using Messenger.db.EF;
using Messenger.db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Core.Services;

public class MessageService : BaseService<Message>, IMessageService
{
    public MessageService(MessengerContext context) : base(context)
    {
    }


    public async Task<Message> Send(Guid chatId, Guid sender, string text)
    {
        throw new NotImplementedException();
    }

    public async Task<Room> GetRoom(Guid chatId, Guid sender)
    {
        throw new NotImplementedException();
    }

    public List<Room> GetUsersRooms(Guid userId)
    {
        throw new NotImplementedException();
    }
}