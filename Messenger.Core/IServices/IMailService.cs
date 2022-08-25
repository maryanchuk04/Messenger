namespace Messenger.Core.IServices;

public interface IMailService
{
    public Task WelcomeSending(string userName, string email);

    public Task ChangeMainMessage(string userName, string oldMail, string newMail);
}