namespace SoftPlus_ToDo.DTOs.Tasks
{
    public sealed record TaskResponseDto
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
        public required bool IsCompleted { get; init; }
        public DateTimeOffset? DueDateUtc { get; init; }
        public DateTimeOffset? CompletedAtUtc { get; init; }
        public required DateTimeOffset CreatedAtUtc { get; init; }
        public DateTimeOffset? UpdatedAtUtc { get; init; }
        public Guid? CategoryId { get; init; }
        public string? CategoryName { get; init; }
        public string? CategoryColor { get; init; }
    }
}