namespace Messenger.db.Entities;

public class Message
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public DateTime When { get; set; } = DateTime.Now;

    public Guid SenderId { get; set; }

    public User Sender { get; set; }

    public Guid RoomId { get; set; }
}