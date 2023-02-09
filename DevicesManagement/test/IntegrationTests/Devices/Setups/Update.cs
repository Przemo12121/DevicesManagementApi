namespace IntegrationTests.Devices;

public partial class Update : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<UpdateSetup>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly UpdateSetup _setupFixture;

    HttpClient HttpClient { get; init; }
    User DummyUser { get; init; }
    string DummyUserJwt { get; init; }

    Device OtherDevice { get; init; }
    Device DummyDevice { get; init; }

    public Update(WebApplicationFactory<Program> factory, UpdateSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        DummyUser = setupFixture.DummyUser;
        DummyUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(DummyUser).RawData;

        DummyDevice = new()
        {
            Id = Guid.NewGuid(),
            Address = "127.0.0.1:1010",
            Commands = new(),
            EmployeeId = DummyUser.EmployeeId,
            Messages = new(),
            Name = "dummy device",
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-10)
        };

        OtherDevice = new()
        {
            Id = Guid.NewGuid(),
            Address = "127.0.0.1:3010",
            Commands = new(),
            EmployeeId = DummyUser.EmployeeId,
            Messages = new(),
            Name = "other device",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        using var context = new DevicesManagementContext();
        context.Devices.AddRange(new[] { DummyDevice, OtherDevice });
        context.SaveChanges();

    }

    private string Route(IDatabaseModel entity) => $"/api/devices/{entity.Id}";

    public void Dispose()
    {
        using var context = new DevicesManagementContext();
        context.Devices.RemoveRange(
            context.Devices.Where(d => new[] { DummyDevice, OtherDevice }.Contains(d))
        );
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}

public class UpdateSetup : IDisposable
{
    public User DummyUser { get; init; }
    public AccessLevel DummyAccessLevel { get; init; }

    public UpdateSetup()
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
            EmployeeId = "zwxy23654123",
            Enabled = true,
            Id = Guid.NewGuid(),
            AccessLevel = DummyAccessLevel
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