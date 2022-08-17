namespace Messenger.db.Entities;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Avatar { get; set; } =
        "https://st3.depositphotos.com/1767687/16607/v/450/depositphotos_166074422-stock-illustration-default-avatar-profile-icon-grey.jpg";

    public string Password { get; set; }

    public string Email { get; set; }

    public virtual ICollection<UserRoom> Rooms { get; set; }

    public ICollection<UserToken> RefreshTokens { get; set; }
}