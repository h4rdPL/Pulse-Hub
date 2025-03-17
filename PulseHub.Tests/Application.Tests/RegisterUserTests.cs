using Microsoft.EntityFrameworkCore;
using PulseHub.Application.DTO;
using PulseHub.Application.Helpers;
using PulseHub.Application.Services;
using PulseHub.Core.Interfaces;
using PulseHub.Infrastructure.Data;
using PulseHub.Infrastructure.Repositories;

namespace PulseHub.Tests.Application.Tests
{
    public class RegisterUserTests
    {
        private readonly RegisterUser _registerUser;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserRepository _userRepository;

        public RegisterUserTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;

            _dbContext = new ApplicationDbContext(options);
            _userRepository = new UserRepository(_dbContext);

            _registerUser = new RegisterUser(_userRepository);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnSuccess_WhenUserIsValid()
        {
            // Arrange
            var userDto = new RegisterUserDTO
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = PasswordHasher.Hash("securepassword")
            };

            // Act
            var result = await _registerUser.ExecuteAsync(userDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Operation successful.", result.Message);

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            Assert.NotNull(user);
            Assert.Equal(userDto.Username, user.Username);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnError_WhenEmailIsInvalid()
        {
            // Arrange
            var invalidUserDto = new RegisterUserDTO
            {
                Username = "testuser",
                Email = "invalid-email",
                Password = PasswordHasher.Hash("password")
            };

            // Act
            var result = await _registerUser.ExecuteAsync(invalidUserDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid email format.", result.Message);
        }
    }
}
