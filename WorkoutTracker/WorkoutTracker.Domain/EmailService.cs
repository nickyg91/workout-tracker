using System.Net.Mail;
using System.Threading.Tasks;
using WorkoutTracker.Data.Entities;

namespace WorkoutTracker.Domain
{
    public class EmailService : IEmailService
    {
        public void SendAccountVerificationEmail(WorkoutUser user)
        {
            var link = $"https://localhost:5001/account/verify/{user.ValidationToken}";
            using var smtpClient = new SmtpClient("localhost");
            var message = new MailMessage("no-reply@workout-tracker.com", user.Email)
            {
                Body = $"Follow the link to verify your account: {link}"
            };
            smtpClient.Send(message);
        }
    }
}
