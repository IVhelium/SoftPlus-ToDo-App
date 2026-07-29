namespace SoftPlus_ToDo.DTOs.Tasks
{
    public sealed record ChangeTaskStatusRequestDto
    {
        public bool IsCompleted { get; init; }
    }
}