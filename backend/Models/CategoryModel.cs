namespace SoftPlus_ToDo.Models
{
    public sealed class CategoryModel : BaseModel
    {
        public required string Name { get; set; }
        public string? Color { get; set; }

        // Foreign Key
        public Guid UserId { get; set; }

        // Navigation Property
        public AppUsersModel User { get; set; } = null!;

        public ICollection<TaskModel> Tasks { get; set; } = [];
    }
}