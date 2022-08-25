using Messenger.db.Entities;

namespace Messenger.Core.DTOs;

public class UserDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Avatar { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public virtual IEnumerable<UserRoom> Rooms { get; set; }

    public IEnumerable<UserToken> RefreshTokens { get; set; }
}