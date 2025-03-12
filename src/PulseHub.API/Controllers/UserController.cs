using Microsoft.AspNetCore.Mvc;
using PulseHub.Application.DTO;
using PulseHub.Application.Results;
using PulseHub.Application.Services;

namespace PulseHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly RegisterUser _registerUser;

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
                return BadRequest(new Result(false, "User data is required.", null));
            }

            var result = await _registerUser.ExecuteAsync(userDTO);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
