
using Database.Models.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace T_Authentication.T_Jwt.T_JwtBearerProvider;

public class T_Generate
{
    private User DummyUser = new User()
    {
        CreatedDate = DateTime.UtcNow,
        UpdatedDate = DateTime.UtcNow,
        Enabled = true,
        EmployeeId = "abcd12345678",
        Name = "Dummy Name",
        Id = new Guid(),
        AccessLevel = new AccessLevel()
        {
            Value = AccessLevels.Admin,
            Description = "Dummy",
            Id = new Guid()
        }
    };

    private JwtOptions Options { get; init; } = new JwtOptions()
    {
        Algorithm = SecurityAlgorithms.HmacSha256,
        Audience = "dummy-audience",
        Issuer = "dummy-issuer",
        ExpirationMs = 50000000,
        Secret = "abcd1234efgh5678"
    };

    private JwtBearerProvider Provider { get; init; }

    public T_Generate()
    {
        Provider = new JwtBearerProvider(Options);
    }

    [Fact]
    public void Generate_GivenUser_CreatesJwtWithSetIssuer()
    {
        var jwt = Provider.Generate(DummyUser);

        jwt.Issuer.Should().Be(Options.Issuer);
    }

    [Fact]
    public void Generate_GivenUser_CreatesJwtWithSetAudience()
    {
        var jwt = Provider.Generate(DummyUser);

        jwt.Audiences.Should().Contain(Options.Audience);
    }

    [Fact]
    public void Generate_GivenUser_CreatesJwtWithUserEmployeeIdAsNameClaim()
    {

        var jwt = Provider.Generate(DummyUser);

        jwt.Claims.ToList()
            .Find(claim => claim.Type.Equals(ClaimTypes.Name))?
            .Value
            .Should()
            .Be(DummyUser.EmployeeId);
    }

    [Fact]
    public void Generate_GivenUser_CreatesJwtWithUserAccessLeveldAsRoleClaim()
    {

        var jwt = Provider.Generate(DummyUser);

        jwt.Claims.ToList()
            .Find(claim => claim.Type.Equals(ClaimTypes.Role))?
            .Value
            .Should()
            .Be(DummyUser.AccessLevel.Value.ToString());
    }
}
