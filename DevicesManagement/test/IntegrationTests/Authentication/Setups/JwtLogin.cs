using Microsoft.AspNetCore.Identity;

namespace IntegrationTests.Authentication;

public partial class JwtLogin : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<JwtLoginSetup>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly JwtLoginSetup _setupFixture;

    private HttpClient HttpClient { get; init; }
    private User DummyUser { get; init; }

    public JwtLogin(WebApplicationFactory<Program> factory, JwtLoginSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        DummyUser = setupFixture.DummyUser;
    }

    private string Route { get; } = "/api/authentication/jwt/login";

    private LoginWithCredentialsRequest ValidRequest { get; } = new()
    {
        Login = "xyzw87654321",
        Password = "dummyPWD123"
    };

    private LoginWithCredentialsRequest WrongLoginRequest { get; } = new()
    {
        Login = "badw87654321",
        Password = "dummyPWD123"
    };

    private LoginWithCredentialsRequest WrongPasswordRequest { get; } = new()
    {
        Login = "xyzw87654321",
        Password = "badPWD123"
    };
}

public class JwtLoginSetup : IDisposable
{ 
    public User DummyUser { get; init; }
    public AccessLevel DummyAccessLevel { get; init; }
    private LocalAuthStorageContext Context { get; init; }

    public JwtLoginSetup()
    {
        // Seed
        Context = new LocalAuthStorageContext();
        DummyAccessLevel = new AccessLevel()
        {
            Value = Database.Models.Enums.AccessLevels.Employee,
            Id = Guid.NewGuid(),
            Description = "dummy"
        };
        DummyUser = new User()
        {
            Name = "Dummy user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            EmployeeId = "xyzw87654321",
            Enabled = true,
            Id = Guid.NewGuid(),
            AccessLevel = DummyAccessLevel
        };
        DummyUser.PasswordHashed = new PasswordHasher<User>().HashPassword(DummyUser, "dummyPWD123");
        Context.AccessLevels.Add(DummyAccessLevel);
        Context.Users.Add(DummyUser);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Users.Remove(DummyUser);
        Context.AccessLevels.Remove(DummyAccessLevel);
        Context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}
