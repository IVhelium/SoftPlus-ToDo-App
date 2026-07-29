using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftPlus_ToDo.Models;

namespace SoftPlus_ToDo.Data.Configurations
{
    public class CategoryModelConfiguration : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => new 
            { 
                c.UserId, 
                c.Name 
            })
            .IsUnique();

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.Color)
                .HasMaxLength(7);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}