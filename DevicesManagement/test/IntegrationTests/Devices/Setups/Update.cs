namespace IntegrationTests.Devices;

public partial class Update : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetupFixture>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetupFixture _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string DummyUserJwt { get; init; }

    Device OtherDevice { get; init; }
    Device DummyDevice { get; init; }

    public Update(WebApplicationFactory<Program> factory, BaseSetupFixture setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;
        _setupFixture.Init(factory);

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        DummyUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        DummyDevice = new()
        {
            Id = Guid.NewGuid(),
            Address = "127.0.0.1:1010",
            Commands = new(),
            EmployeeId = RequestingUser.EmployeeId,
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
            EmployeeId = RequestingUser.EmployeeId,
            Messages = new(),
            Name = "other device",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        using var context = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
            );
        context.Devices.AddRange(new[] { DummyDevice, OtherDevice });
        context.SaveChanges();

    }

    private string Route(IDatabaseModel entity) => $"/api/devices/{entity.Id}";

    public void Dispose()
    {
        using var context = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        context.Devices.RemoveRange(
            context.Devices.Where(d => new[] { DummyDevice, OtherDevice }.Contains(d))
        );
        context.SaveChanges();

        _setupFixture.Clear();

        GC.SuppressFinalize(this);
    }
}