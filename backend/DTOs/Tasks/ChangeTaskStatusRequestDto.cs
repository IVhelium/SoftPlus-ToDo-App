namespace SoftPlus_ToDo.DTOs.Tasks
{
    public sealed record ChangeTaskStatusRequestDto
    {
        public required bool IsCompleted { get; init; }
    }
}