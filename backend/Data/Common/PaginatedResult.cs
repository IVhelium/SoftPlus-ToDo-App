namespace SoftPlus_ToDo.Data.Common
{
    public sealed record PaginatedResult<T>
    {
        public IReadOnlyCollection<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
    }
}