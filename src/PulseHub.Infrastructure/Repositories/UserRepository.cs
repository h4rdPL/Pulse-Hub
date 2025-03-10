using PulseHub.Core.Entities;
using PulseHub.Core.Interfaces;
using PulseHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PulseHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users
                                 .AnyAsync(u => u.Email == email); 
        }
    }
}
