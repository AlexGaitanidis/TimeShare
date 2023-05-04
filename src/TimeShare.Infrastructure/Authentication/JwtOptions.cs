namespace TimeShare.Infrastructure.Authentication;

public class JwtOptions
{
    public const string SectionName = "JwtOptions";
    public string Secret { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}