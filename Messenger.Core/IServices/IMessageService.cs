using Messenger.db.Entities;

namespace Messenger.Core.IServices;

public interface IMessageService
{
    Task<Message> Send(Guid chatId, Guid sender, string text);

    Task<Room> GetRoom(Guid chatId, Guid sender);

    List<Room> GetUsersRooms(Guid userId);
}