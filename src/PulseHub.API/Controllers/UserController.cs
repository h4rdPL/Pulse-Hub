using Microsoft.AspNetCore.Mvc;
using PulseHub.Application.DTO;
using PulseHub.Application.Results;
using PulseHub.Application.Services;

namespace PulseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RegisterUser _registerUser;
        private readonly LoginUser _loginUser;
        public UserController(RegisterUser registerUser)
        {
            _registerUser = registerUser;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="userDTO">The user data transfer object containing the user information (username, email, password).</param>
        /// <returns>
        /// Returns an HTTP status code along with a result object:
        /// - 200 OK with success message if user registration is successful.
        /// - 400 BadRequest with error message if user registration fails.
        /// </returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest(Result<string>.Failure("User data is required.", "400"));
            }

            var result = await _registerUser.ExecuteAsync(userDTO);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Logs in a user using the provided credentials and returns a JWT
        /// </summary>
        /// <param name="loginUserDTO">The login credentials of the user (email and password).</param>
        /// <returns>An IActionResult containing either a token if successful or an error message if login fails.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            if (loginUserDTO == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await _loginUser.ExecuteAsync(loginUserDTO);

            if (result.IsSuccess)
            {
                return Ok(new { Token = result.Data });
            }

            return Unauthorized(new { Message = result.Message });
        }
    }
}
