using PulseHub.Core.Entities;

namespace PulseHub.Core.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(string email);
    }
}
