namespace Messenger.db.Entities;

public class UserRoom
{
    public Guid  Id { get; set; }

    public Guid UserId { get; set; }

    public virtual User  User { get; set; }

    public Guid RoomId { get; set; }

    public virtual Room Room { get; set; }

}