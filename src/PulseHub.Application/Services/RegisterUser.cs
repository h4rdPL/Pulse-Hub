using PulseHub.Application.DTO;
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

        public async Task<Result> ExecuteAsync(RegisterUserDTO userDto)
        {
            if (await _userRepository.UserExistsAsync(userDto.Email))
            {
                return Result.Failure("User already exists.");
            }

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password 
            };

            await _userRepository.AddUserAsync(user);

            return Result.Success("User registered successfully.");
        }
    }

}
