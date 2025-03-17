using Moq;
using PulseHub.Application.DTO;
using PulseHub.Application.Helpers;
using PulseHub.Application.Services;
using PulseHub.Core.Entities;
using PulseHub.Core.Interfaces;

namespace PulseHub.Tests.Application.Tests
{
    public class LoginUserTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository; 
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly LoginUser _loginUser;

        public LoginUserTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockTokenService = new Mock<ITokenService>();

            _loginUser = new LoginUser(_mockUserRepository.Object, _mockTokenService.Object);
        }

        [Fact]
        public async Task LoginUser_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var hashedPassword = PasswordHasher.Hash("password");

            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Email = "test@example.com", Password = hashedPassword });

            // Setup mock for Verify method
            _mockTokenService.Setup(service => service.GenerateToken(It.IsAny<User>())).Returns("sample-token");

            var loginDTO = new LoginUserDTO { Email = "test@example.com", Password = "password" };
            var result = await _loginUser.ExecuteAsync(loginDTO);

            Assert.True(result.IsSuccess);
            Assert.Equal("sample-token", result.Data);
        }


    }
}
