using System.Threading.Tasks;

namespace Solen.Infrastructure.Notifications.Emailing
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Email email);
    }
}