using PulseHub.Application.Results;
using PulseHub.Core.Interfaces;

namespace PulseHub.Application.Services
{
    public class NotificationService
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public NotificationService(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> SendNotificationAsync(string userEmail, string message)
        {
            var user = await _userRepository.GetUserByEmailAsync(userEmail);
            if (user == null)
            {
                return Result<bool>.Failure("User not found.");
            }

            var emailResult = await _emailService.SendEmailAsync(user.Email, "Notification", message);
            if (!emailResult)
            {
                return Result<bool>.Failure("Failed to send email notification.");
            }

            return Result<bool>.Success(true);
        }
    }
}
