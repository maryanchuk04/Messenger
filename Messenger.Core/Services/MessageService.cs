using Messenger.Core.IServices;
using Messenger.db.EF;
using Messenger.db.Entities;

namespace Messenger.Core.Services;

public class MessageService : BaseService<Message>, IMessageService
{
    public MessageService(MessengerContext context) : base(context)
    {
    }


}