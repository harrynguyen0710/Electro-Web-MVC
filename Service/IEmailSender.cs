using DACS.Models;

namespace DACS.Service
{
    public interface IEmailSender
    {
        Task SendMail(MailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
