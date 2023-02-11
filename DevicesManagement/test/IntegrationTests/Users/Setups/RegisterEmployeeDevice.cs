namespace IntegrationTests.Users;

public partial class RegisterEmployeeDevice : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<RegisterEmployeeDeviceSetup>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly RegisterEmployeeDeviceSetup _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    string Route(IDatabaseModel entity) => $"api/users/employees/{entity.Id}/devices";

    static RegisterDeviceRequest DummyRequest { get; } = new()
    {
        Name = "Dummy test register device",
        Address = "127.0.0.1:5060"
    };

    public RegisterEmployeeDevice(WebApplicationFactory<Program> factory, RegisterEmployeeDeviceSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        RequestingUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;
    }

    public void Dispose()
    {
        using var context = new DevicesManagementContext();
        context.Devices.RemoveRange(
            context.Devices.Where(
                d => d.EmployeeId.Equals(RequestingUser.EmployeeId) && d.Name.Equals(DummyRequest.Name)
            )
        );
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}

public class RegisterEmployeeDeviceSetup : IDisposable
{
    public User RequestingUser { get; init; }
    public AccessLevel AdminAccessLevel { get; init; }
    public RegisterEmployeeDeviceSetup()
    {
        // Seed
        AdminAccessLevel = new AccessLevel()
        {
            Value = Database.Models.Enums.AccessLevels.Admin,
            Id = Guid.NewGuid(),
            Description = "dummy"
        };
        RequestingUser = new User()
        {
            Name = "Dummy user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            EmployeeId = "zwxy23654322",
            Enabled = true,
            Id = Guid.NewGuid(),
            AccessLevel = AdminAccessLevel,
        };
        RequestingUser.PasswordHashed = new PasswordHasher<User>().HashPassword(RequestingUser, "dummyPWD123");

        using var context = new LocalAuthStorageContext();
        context.AccessLevels.Add(AdminAccessLevel);
        context.Users.Add(RequestingUser);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.Remove(RequestingUser);
        context.AccessLevels.Remove(AdminAccessLevel);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}