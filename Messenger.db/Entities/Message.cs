namespace Messenger.db.Entities;

public class Message
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public DateTime When { get; set; } = DateTime.Now;

    public Guid FromUserId { get; set; }

    public Guid RoomId { get; set; }

    public virtual Room Room { get; set; }
}