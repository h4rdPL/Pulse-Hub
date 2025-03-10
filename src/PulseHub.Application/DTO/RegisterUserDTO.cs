namespace PulseHub.Application.DTO
{
    public record struct RegisterUserDTO(
            string Username,
            string Email,
            string Password
        );
}
