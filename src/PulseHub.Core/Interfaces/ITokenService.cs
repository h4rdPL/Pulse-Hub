using PulseHub.Core.Entities;

namespace PulseHub.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
