namespace Messenger.db.Bridge;

public interface ISecurityContext
{
    Guid GetCurrentUserId();
}