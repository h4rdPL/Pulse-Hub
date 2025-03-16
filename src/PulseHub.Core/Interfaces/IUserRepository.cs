using PulseHub.Core.Entities;

namespace PulseHub.Core.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string email);
    }
}
