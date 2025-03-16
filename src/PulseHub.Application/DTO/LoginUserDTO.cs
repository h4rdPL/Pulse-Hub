namespace PulseHub.Application.DTO
{
    public record struct LoginUserDTO(
        string Email,
        string Password
    );
}
