namespace SoftPlus_ToDo.Models
{
    public sealed class TaskModel : BaseModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTimeOffset? DueDateUtc { get; set;}
        public DateTimeOffset? CompletedAtUtc { get; set; }

        // Foreign Key
        public Guid UserId { get; set; }
        public Guid? CategoryId { get; set; }

        // Navigation Property
        public AppUsersModel User { get; set; } = null!;
        public CategoryModel? Category { get; set; }
    }
}