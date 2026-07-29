namespace SoftPlus_ToDo.Models
{
    public sealed class RefreshTokenModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiryTime;

        // Foreign Key
        public Guid UserId { get; set; }

        // Navigation Property
        public AppUsersModel User { get; set; } = null!;
    }
}