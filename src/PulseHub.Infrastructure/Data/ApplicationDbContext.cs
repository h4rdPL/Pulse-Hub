﻿using Microsoft.EntityFrameworkCore;
using PulseHub.Core.Entities;

namespace PulseHub.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
    }
}
