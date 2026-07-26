namespace SoftPlus_ToDo.Options
{
    public sealed record JwtOptions
    {
        public string Secret { get; init; } = string.Empty;
        public string CookieName { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public int ExpiryInMinutes { get; init; }
    }
}