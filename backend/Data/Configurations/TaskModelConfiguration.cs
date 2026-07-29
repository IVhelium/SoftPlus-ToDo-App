using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Data.Configurations
{
    public sealed class TaskModelConfiguration : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => new
            {
                t.UserId,
                t.CategoryId,
                t.IsCompleted
            });

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(3000);

            builder.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}