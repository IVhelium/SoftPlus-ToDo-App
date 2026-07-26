using Microsoft.AspNetCore.Identity;

namespace SoftPlus_ToDo.Models
{
    public sealed class AppUsersModel : IdentityUser<Guid>
    {
        public string? Description { get; set; }
        public string? AvatarUrl { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAtUtc { get; set; }

        // Navigation Property
        public ICollection<RefreshTokenModel> RefreshTokens = [];
    }
}