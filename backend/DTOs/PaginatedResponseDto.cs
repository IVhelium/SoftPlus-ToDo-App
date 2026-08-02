namespace SoftPlus_ToDo.DTOs
{
    public sealed record PaginatedResponseDto<T>
    {
        public IReadOnlyCollection<T> Items { get; init; } = [];
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }
        public bool HasPreviousPage { get; init; }
        public bool HasNextPage { get; init; }
    }
}