using Microsoft.AspNetCore.Identity;
using SoftPlus_ToDo.Data;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddApiIdentity(
            this IServiceCollection services
        )
        {
            services
                .AddIdentityCore<AppUsersModel>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout Settings
                options.Lockout.AllowedForNewUsers = false;

                // User Settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.";
                options.User.RequireUniqueEmail = true;
            });
        }
    }
}