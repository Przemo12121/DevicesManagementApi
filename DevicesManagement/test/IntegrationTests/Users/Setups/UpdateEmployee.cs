namespace IntegrationTests.Users;

public partial class UpdateEmployee : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DeleteEmployeeSetup>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly DeleteEmployeeSetup _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    User OtherUser { get; init; }
    User DummyUser { get; init; }

    public UpdateEmployee(WebApplicationFactory<Program> factory, DeleteEmployeeSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        RequestingUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        DummyUser = new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = "abab12121212",
            Name = "dummy user",
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-10),
            AccessLevelId = _setupFixture.DummyAccessLevel.Id,
            PasswordHashed = "not important"
        };

        OtherUser = new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = "cdcd23232323",
            Name = "other user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            AccessLevelId = _setupFixture.DummyAccessLevel.Id,
            PasswordHashed = "not important"
        };

        using var context = new LocalAuthStorageContext();
        context.Users.AddRange(new[] { DummyUser, OtherUser });
        context.SaveChanges();
    }

    private string Route(IDatabaseModel entity) => $"/api/users/employees/{entity.Id}";

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.RemoveRange(
            context.Users.Where(d => new[] { DummyUser, OtherUser }.Contains(d))
        );
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}

public class UpdateEmployeeSetup : IDisposable
{
    public User DummyUser { get; init; }
    public AccessLevel DummyAccessLevel { get; init; }

    public UpdateEmployeeSetup()
    {
        // Seed
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
            EmployeeId = "zwxy23654322",
            Enabled = true,
            Id = Guid.NewGuid(),
            AccessLevel = DummyAccessLevel,
        };
        DummyUser.PasswordHashed = new PasswordHasher<User>().HashPassword(DummyUser, "dummyPWD123");

        using var context = new LocalAuthStorageContext();
        context.AccessLevels.Add(DummyAccessLevel);
        context.Users.Add(DummyUser);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.Remove(DummyUser);
        context.AccessLevels.Remove(DummyAccessLevel);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}