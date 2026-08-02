namespace SoftPlus_ToDo.DTOs.Categories
{
    public sealed record CategoryResponseDto
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public string? Color { get; init; }
        public int TaskCount { get; init; }
        public DateTimeOffset CreatedAtUtc { get; init; }
        public DateTimeOffset? UpdatedAtUtc { get; init; }
    }
}