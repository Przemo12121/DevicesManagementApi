namespace IntegrationTests.Commands;

public partial class Delete : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetupFixture>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetupFixture _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string DummyUserJwt { get; init; }

    Command DummyCommand { get; init; }
    Command OtherCommand { get; init; }
    Device DummyDevice { get; init; }

    public Delete(WebApplicationFactory<Program> factory, BaseSetupFixture setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        DummyUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        DummyCommand = new()
        {
            Body = "dummy body",
            CommandHistories = new(),
            Id = Guid.NewGuid(),
            Name = "dummy command",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Description = "dummy description"
        };
        OtherCommand = new()
        {
            Body = "other body",
            CommandHistories = new(),
            Id = Guid.NewGuid(),
            Name = "other command",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Description = "other description"
        };
        DummyDevice = new()
        {
            Id = Guid.NewGuid(),
            Address = "127.0.0.1:1010",
            Commands = new() { DummyCommand, OtherCommand },
            EmployeeId = RequestingUser.EmployeeId,
            Messages = new(),
            Name = "dummy device",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        using var context = new DevicesManagementContext();
        context.Commands.AddRange(DummyDevice.Commands);
        context.Devices.Add(DummyDevice);
        context.SaveChanges();

    }

    private string Route(IDatabaseModel entity) => $"/api/commands/{entity.Id}";

    public void Dispose()
    {
        using var context = new DevicesManagementContext();
        context.Commands.RemoveRange(
            context.Commands.Where(c => DummyDevice.Commands.Contains(c)).ToArray()
        );
        context.Devices.RemoveRange(
            context.Devices.Where(d => d.Equals(DummyDevice))
        );
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}