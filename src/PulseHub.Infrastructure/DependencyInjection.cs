using PulseHub.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PulseHub.Application.Services;
using PulseHub.Core.Interfaces;
using PulseHub.Infrastructure.Data;
using PulseHub.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using PulseHub.Core.Entities;

namespace PulseHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenServices>();
            services.AddScoped<RegisterUser>();
            services.AddScoped<LoginUser>();
            services.AddScoped<NotificationService>();
            services.AddScoped<INotificationHub, NotificationHubService>();
        }
    }
}

