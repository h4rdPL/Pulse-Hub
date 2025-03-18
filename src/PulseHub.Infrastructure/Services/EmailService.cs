using PulseHub.Core.Interfaces;

namespace PulseHub.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail) ||
                string.IsNullOrWhiteSpace(subject) ||
                string.IsNullOrWhiteSpace(body))
            {
                return false;
            }

            await Task.Delay(100);
            return true;
        }
    }
}
