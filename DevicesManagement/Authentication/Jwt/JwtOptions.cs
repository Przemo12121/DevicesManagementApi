namespace Authentication.Jwt;

public sealed record JwtOptions
{
    public string Audience { get; init; }
    public string Issuer { get; init; }
    public UInt64 ExpirationMs { get; init; }
    public string Secret { get; init; }
    public string Algorithm { get; init; }
}
