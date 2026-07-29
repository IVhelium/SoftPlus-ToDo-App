using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SoftPlus_ToDo.Models;
using System.Reflection;

namespace SoftPlus_ToDo.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityUserContext<AppUsersModel, Guid>(options)
    {
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Finds any classes that implement IEntityTypeConfiguration and applies them
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}