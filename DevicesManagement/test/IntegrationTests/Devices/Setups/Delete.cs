namespace IntegrationTests.Devices;

public partial class Delete : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetup>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetup _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string DummyUserJwt { get; init; }

    Device OtherDevice { get; init; }
    Device DummyDevice { get; init; }

    public Delete(WebApplicationFactory<Program> factory, BaseSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

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
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        OtherDevice = new()
        {
            Id = Guid.NewGuid(),
            Address = "127.0.0.1:3010",
            Commands = new(),
            EmployeeId = RequestingUser.EmployeeId,
            Messages = new(),
            Name = "dummy device",
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