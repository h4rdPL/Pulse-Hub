using PulseHub.Application.DTO;
using PulseHub.Application.Helpers;
using PulseHub.Application.Results;
using PulseHub.Core.Entities;
using PulseHub.Core.Interfaces;

namespace PulseHub.Application.Services
{
    public class RegisterUser
    {
        private readonly IUserRepository _userRepository;

        public RegisterUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<string>> ExecuteAsync(RegisterUserDTO userDto)
        {
            if (!EmailValidator.IsValidEmail(userDto.Email))
            {
                return Result<string>.Failure("Invalid email format.", "400");
            }
            if (await _userRepository.UserExistsAsync(userDto.Email))
            {
                return Result<string>.Failure("User already exists.", "409");
            }

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password
            };

            await _userRepository.AddUserAsync(user);

            return Result<string>.Success("User registered successfully.");
        }
    }
}
