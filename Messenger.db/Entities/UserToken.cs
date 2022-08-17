using System.ComponentModel.DataAnnotations;

namespace Messenger.db.Entities;

public class UserToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public string  Token { get; set; }

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Revoked { get; set; }

    public string? ReplacedByToken { get; set; }

}