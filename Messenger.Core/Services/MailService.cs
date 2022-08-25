using MailKit.Security;
using Messenger.Core.IServices;
using MimeKit;

namespace Messenger.Core.Services;

public class MailService : IMailService
{
    public async Task WelcomeSending(string userName, string email)
    {
        try
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("MaxiMessenger","lion20914king@gmail.com"));
            message.To.Add(new MailboxAddress(userName, email));
            message.Subject = $"Welcome {userName} to MaxiMessenger {email}";
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("lion20914king@gmail.com", "vafwlujvtzimuxyj");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (Exception)
        {
            throw new Exception("Error in message");
        }
    }

    public async Task ChangeMainMessage(string userName, string oldMail, string newMail)
    {
        try
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("MaxiMessenger","lion20914king@gmail.com"));
            message.To.Add(new MailboxAddress(userName, newMail));
            message.To.Add(new MailboxAddress(userName, oldMail));
            message.Subject = $"Dear {userName}!";
            message.Body = new BodyBuilder { TextBody = "MaxiMessenger! Your account Email was changed"}.ToMessageBody();
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("lion20914king@gmail.com", "vafwlujvtzimuxyj");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (Exception)
        {
            throw new Exception("Error in message");
        }
    }
}