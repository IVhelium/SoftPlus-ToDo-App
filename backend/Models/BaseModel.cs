namespace SoftPlus_ToDo.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAtUtc { get; set; }
    }
}