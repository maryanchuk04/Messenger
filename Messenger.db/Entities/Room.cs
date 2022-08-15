namespace Messenger.db.Entities;

public class Room
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<Message> Messages { get; set; }
}