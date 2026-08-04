namespace SoftPlus_ToDo.DTOs
{
    public sealed record ExceptionResponseDto
    {
        public required int StatusCode { get; init; }
        public required string Title { get; init; }
        public required string Detail { get; init; }
    }
}