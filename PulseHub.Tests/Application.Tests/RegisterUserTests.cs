using Moq;
using PulseHub.Application.DTO;
using PulseHub.Application.Services;
using PulseHub.Core.Entities;
using PulseHub.Core.Interfaces;

namespace PulseHub.Tests.Application.Tests
{
    public class RegisterUserTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly RegisterUser _registerUser;

        public RegisterUserTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _registerUser = new RegisterUser(_userRepositoryMock.Object);
        }


        [Fact]
        public async Task RegisterUser_ShouldReturnSuccess_WhenUserIsValid()
        {
            // Arrange
            var userDto = new RegisterUserDTO
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "securepassword"
            };

            _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _registerUser.ExecuteAsync(userDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User registered successfully.", result.Message);
        }

    }
}
