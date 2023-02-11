namespace IntegrationTests.Users;

public partial class DeleteEmployee : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DeleteEmployeeSetup>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly DeleteEmployeeSetup _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string DummyUserJwt { get; init; }

    User OtherUser { get; init; }
    User DummyUser { get; init; }

    public DeleteEmployee(WebApplicationFactory<Program> factory, DeleteEmployeeSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        DummyUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        DummyUser = new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = "xyxy56565656",
            Name = "dummy user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            AccessLevelId = _setupFixture.DummyAccessLevel.Id,
            PasswordHashed = "not important"
        };

        OtherUser = new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = "stst78787878",
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

public class DeleteEmployeeSetup : IDisposable
{
    public User RequestingUser { get; init; }
    public AccessLevel DummyAccessLevel { get; init; }

    public DeleteEmployeeSetup()
    {
        // Seed
        DummyAccessLevel = new AccessLevel()
        {
            Value = Database.Models.Enums.AccessLevels.Admin,
            Id = Guid.NewGuid(),
            Description = "dummy"
        };
        RequestingUser = new User()
        {
            Name = "ummy user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            EmployeeId = "zwxy23555822",
            Enabled = true,
            Id = Guid.NewGuid(),
            AccessLevel = DummyAccessLevel
        };
        RequestingUser.PasswordHashed = new PasswordHasher<User>().HashPassword(RequestingUser, "dummyPWD123");

        using var context = new LocalAuthStorageContext();
        context.AccessLevels.Add(DummyAccessLevel);
        context.Users.Add(RequestingUser);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.Remove(RequestingUser);
        context.AccessLevels.Remove(DummyAccessLevel);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}