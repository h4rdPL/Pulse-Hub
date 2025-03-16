﻿using PulseHub.Application.DTO;
using PulseHub.Application.Results;
using PulseHub.Core.Interfaces;

namespace PulseHub.Application.Services
{
    public class LoginUser
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginUser(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> ExecuteAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginUserDTO.Email);
            if (user == null || user.Password != loginUserDTO.Password)
            {
                return Result<string>.Failure("Invalid credentials");
            }

            var token = _tokenService.GenerateToken(user);
            return Result<string>.Success(token);
        }
    }
}
