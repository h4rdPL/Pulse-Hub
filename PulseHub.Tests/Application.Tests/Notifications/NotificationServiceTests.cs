using Moq;
using PulseHub.Application.Services;
using PulseHub.Core.Entities;
using PulseHub.Core.Interfaces;

namespace PulseHub.Tests.Application.Tests.Notifications
{
    public class NotificationServiceTests
    {
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<INotificationHub> _mockNotificationHub;
        private readonly NotificationService _notificationService;

        public NotificationServiceTests()
        {
            _mockEmailService = new Mock<IEmailService>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockNotificationHub = new Mock<INotificationHub>();
            _notificationService = new NotificationService(_mockEmailService.Object, _mockUserRepository.Object, _mockNotificationHub.Object);
        }

        [Fact]
        public async Task SendNotificationAsync_ShouldReturnSuccess_WhenUserExists()
        {
            var user = new User { Id = 1, Email = "test@example.com" };
            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
            _mockEmailService.Setup(service => service.SendEmailAsync(user.Email, "Notification", "Test message"))
                             .ReturnsAsync(true);

            var result = await _notificationService.SendNotificationAsync(user.Email, "Test message");

            Assert.True(result.IsSuccess);
            _mockEmailService.Verify(service => service.SendEmailAsync(user.Email, "Notification", "Test message"), Times.Once);
        }

        [Fact]
        public async Task SendNotificationAsync_ShouldReturnFailure_WhenUserDoesNotExist()
        {
            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                               .ReturnsAsync((User)null);

            var result = await _notificationService.SendNotificationAsync("non-existent-user@example.com", "Test message");

            Assert.False(result.IsSuccess);
            Assert.Equal("User not found.", result.Message);
            _mockEmailService.Verify(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task SendNotificationAsync_ShouldReturnFailure_WhenEmailSendingFails()
        {
            var user = new User { Id = 1, Email = "test@example.com" };
            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
            _mockEmailService.Setup(service => service.SendEmailAsync(user.Email, "Notification", "Test message"))
                             .ReturnsAsync(false);

            var result = await _notificationService.SendNotificationAsync(user.Email, "Test message");

            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to send email notification.", result.Message);
            _mockEmailService.Verify(service => service.SendEmailAsync(user.Email, "Notification", "Test message"), Times.Once);
        }

        [Fact]
        public async Task SendNotificationAsync_ShouldSendSignalRMessage_WhenUserExists()
        {
            var user = new User { Id = 1, Email = "test@example.com" };
            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
            _mockEmailService.Setup(service => service.SendEmailAsync(user.Email, "Notification", "Test message"))
                             .ReturnsAsync(true);

            var result = await _notificationService.SendNotificationAsync(user.Email, "Test message");

            Assert.True(result.IsSuccess);
            _mockNotificationHub.Verify(hub => hub.SendNotificationAsync(user.Email, "Test message"), Times.Once);
        }

        [Fact]
        public async Task SendNotificationAsync_ShouldReturnFailure_WhenSignalRFails()
        {
            var user = new User { Id = 1, Email = "test@example.com" };
            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
            _mockEmailService.Setup(service => service.SendEmailAsync(user.Email, "Notification", "Test message"))
                             .ReturnsAsync(true);
            _mockNotificationHub.Setup(hub => hub.SendNotificationAsync(user.Email, "Test message"))
                                .ThrowsAsync(new Exception("SignalR failure"));

            var result = await _notificationService.SendNotificationAsync(user.Email, "Test message");

            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to send real-time notification.", result.Message);
        }

        [Fact]
        public async Task SendNotificationAsync_ShouldSendBothEmailAndSignalR_WhenSuccessful()
        {
            var user = new User { Id = 1, Email = "test@example.com" };
            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
            _mockEmailService.Setup(service => service.SendEmailAsync(user.Email, "Notification", "Test message"))
                             .ReturnsAsync(true);

            var result = await _notificationService.SendNotificationAsync(user.Email, "Test message");

            Assert.True(result.IsSuccess);
            _mockEmailService.Verify(service => service.SendEmailAsync(user.Email, "Notification", "Test message"), Times.Once);
            _mockNotificationHub.Verify(hub => hub.SendNotificationAsync(user.Email, "Test message"), Times.Once);
        }
    }
}
