namespace PulseHub.Application.DTO
{
    public record struct SendNotificationRequest(
            string Email,
            string Message
        );
}
