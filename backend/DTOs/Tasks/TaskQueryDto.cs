using System.ComponentModel.DataAnnotations;

namespace SoftPlus_ToDo.DTOs.Tasks
{
    public sealed record TaskQueryDto
    {
        public string? Search { get; init; }
        public Guid? CategoryId { get; init; }
        public bool? IsCompleted { get; init; }
        public DateTimeOffset? DueFromUtc { get; init; }
        public DateTimeOffset? DueToUtc { get; init; }

        [Range(1, int.MaxValue)]
        public int Page { get; init; } = 1;

        [Range(1, 40)]
        public int PageSize { get; init; } = 40;
    }
}