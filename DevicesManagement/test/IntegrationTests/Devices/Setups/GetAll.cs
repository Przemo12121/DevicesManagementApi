namespace IntegrationTests.Devices;

public partial class GetAll : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetupFixture>, IDisposable
{
    WebApplicationFactory<Program> _factory;
    BaseSetupFixture _setupFixture;

    List<User> DummyUsers { get; init; }
    List<Device> FirstUserDevices { get; init; }
    List<Device> SecondUserDevices { get; init; }
    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    string Route { get; } = "api/devices";

    public GetAll(WebApplicationFactory<Program> factory, BaseSetupFixture setupFixture) 
    {
        _setupFixture = setupFixture;
        _factory = factory;
        _setupFixture.Init(factory);

        HttpClient = _factory.CreateClient();
        RequestingUser = _setupFixture.RequestingUser;
        RequestingUserJwt = _factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        DummyUsers = new()
        {
            new()
            {
                AccessLevelId = setupFixture.EmployeeAccessLevel.Id,
                EmployeeId = "aaaa12345678",
                Name = "Zzz-name",
                Enabled = true,
                Id = Guid.NewGuid(),
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                PasswordHashed = "not important"
            },
            new()
            {
                AccessLevelId = setupFixture.EmployeeAccessLevel.Id,
                EmployeeId = "bbbb12345678",
                Name = "Mmm-name",
                Enabled = true,
                Id = Guid.NewGuid(),
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                PasswordHashed = "not important"
            },
        };

        FirstUserDevices = new()
        {
            // first by name, third by address
            new()
            {
                Address = "255.255.255.255:3000",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                EmployeeId = DummyUsers[0].EmployeeId,
                Id = Guid.NewGuid(),
                Name = "Aaa-name"
            },
            // second by name, second by address
            new()
            {
                Address = "155.255.255.255:3000",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                EmployeeId = DummyUsers[0].EmployeeId,
                Id = Guid.NewGuid(),
                Name = "Bbb-name"
            },
            // third by name, first by address
            new()
            {
                Address = "0.255.255.255:3000",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                EmployeeId = DummyUsers[0].EmployeeId,
                Id = Guid.NewGuid(),
                Name = "Zzz-name"
            }
        };
        SecondUserDevices = new()
        {
            new()
            {
                Address = "0.0.0.0:0",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                EmployeeId = DummyUsers[1].EmployeeId,
                Id = Guid.NewGuid(),
                Name = "Ccc-name"
            }
        };


        using var context = new LocalAuthContext(
            _factory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        context.Users.AddRange(DummyUsers);
        context.SaveChanges();

        using var context2 = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        context2.Devices.AddRange(FirstUserDevices);
        context2.Devices.AddRange(SecondUserDevices);
        context2.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthContext(
            _factory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        context.Users.RemoveRange(DummyUsers);
        context.SaveChanges();

        using var context2 = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        context2.Devices.RemoveRange(FirstUserDevices);
        context2.Devices.RemoveRange(SecondUserDevices);
        context2.SaveChanges();

        _setupFixture.Clear();

        GC.SuppressFinalize(this);
    }
}
