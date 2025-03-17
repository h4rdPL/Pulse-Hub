using PulseHub.Application.Helpers;
using Xunit;

namespace PulseHub.Tests.Application.Helpers
{
    public class PasswordHasherTests
    {
        [Theory]
        [InlineData("Weak123", false)]  
        [InlineData("password123!", false)] 
        [InlineData("PASSWORD123!", false)] 
        [InlineData("Short1!", false)] 
        [InlineData("StrongPass1!", true)] 
        public void IsValidPassword_ShouldReturnExpectedResult(string password, bool expected)
        {
            var result = PasswordHasher.IsValidPassword(password);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void HashPassword_ShouldThrowException_WhenPasswordIsInvalid()
        {
            var invalidPassword = "weak"; 

            var exception = Assert.Throws<ArgumentException>(() => PasswordHasher.HashPassword(invalidPassword));
            Assert.Equal("Password does not meet security requirements.", exception.Message);
        }

        [Fact]
        public void HashPassword_ShouldReturnHashedPassword_WhenPasswordIsValid()
        {
            var validPassword = "StrongPass1!";
            var hashedPassword = PasswordHasher.HashPassword(validPassword);

            Assert.NotNull(hashedPassword);
            Assert.NotEqual(validPassword, hashedPassword);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_WhenPasswordsMatch()
        {
            var password = "StrongPass1!";
            var hashedPassword = PasswordHasher.HashPassword(password);

            var result = PasswordHasher.VerifyPassword(password, hashedPassword);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_WhenPasswordsDoNotMatch()
        {
            var password = "StrongPass1!";
            var wrongPassword = "WrongPassword!";
            var hashedPassword = PasswordHasher.HashPassword(password);

            var result = PasswordHasher.VerifyPassword(wrongPassword, hashedPassword);

            Assert.False(result);
        }
    }
}
