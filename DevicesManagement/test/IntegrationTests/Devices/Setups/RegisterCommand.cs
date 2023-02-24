namespace IntegrationTests.Devices;

public partial class RegisterCommand : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetupFixture>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetupFixture _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    Device DummyDevice { get; init; }

    RegisterCommandRequest DummyRequest { get; } = new()
    {
        Name = "test register command",
        Body = "dummy body",
        Description = "dummy description"
    };

    public RegisterCommand(WebApplicationFactory<Program> factory, BaseSetupFixture setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        RequestingUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

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

        using var context = new DevicesManagementContext();
        context.Devices.Add(DummyDevice);
        context.SaveChanges();

    }

    private string Route(IDatabaseModel entity) => $"/api/devices/{entity.Id}/commands";

    public void Dispose()
    {
        using var context = new DevicesManagementContext();
        context.Commands.RemoveRange(
            context.Commands.Where(
                c => c.Name.Equals(DummyRequest.Name) && c.Body.Equals(DummyRequest.Body)
            )
        );
        context.Devices.RemoveRange(
            context.Devices.Where(d => d.Equals(DummyDevice))
        );
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}