namespace IntegrationTests.Devices;

public partial class GetCommands : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetup>, IDisposable
{
    WebApplicationFactory<Program> _factory;
    BaseSetup _setupFixture;

    Device FirstDevice { get; init; }
    Device SecondDevice { get; init; }
    List<Command> FirstDeviceCommands { get; init; }
    List<Command> SecondDeviceCommands { get; init; }
    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    string Route(IDatabaseModel entity) => $"api/devices/{entity.Id}/commands";

    public GetCommands(WebApplicationFactory<Program> webApplicationFactory, BaseSetup setupFixture) 
    {
        _setupFixture = setupFixture;
        _factory = webApplicationFactory;

        HttpClient = _factory.CreateClient();
        RequestingUser = _setupFixture.RequestingUser;
        RequestingUserJwt = _factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        FirstDeviceCommands = new()
        {
            // first by name, third by body
            new()
            {
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "Aaa-name",
                Body = "Zzz command"
            },
            // second by name, second by body
            new()
            {
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "Bbb-name",
                Body = "Bbb command"
            },
            // third by name, first by body
            new()
            {
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "Zzz-name",
                Body = "Aaa command"
            }
        };
        SecondDeviceCommands = new()
        {
            new()
            {
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "Ccc-name",
                Body = "Ccc command"
            }
        };

        FirstDevice = new()
        {
            EmployeeId = "aaaa12345678",
            Name = "Zzz-name",
            Id = Guid.NewGuid(),
            UpdatedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            Address = "127.0.0.1:3000",
            Commands = FirstDeviceCommands
        };
        SecondDevice = new()
        {
            EmployeeId = "aaaa12345678",
            Name = "Yyy-name",
            Id = Guid.NewGuid(),
            UpdatedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
            Address = "127.0.0.1:3001",
            Commands = SecondDeviceCommands
        };

        using var context = new DevicesManagementContext();
        context.Commands.AddRange(SecondDeviceCommands);
        context.Commands.AddRange(SecondDeviceCommands);
        context.Devices.Add(FirstDevice);
        context.Devices.Add(SecondDevice);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new DevicesManagementContext();
        context.Commands.RemoveRange(FirstDeviceCommands);
        context.Commands.RemoveRange(SecondDeviceCommands);
        context.Devices.Remove(FirstDevice);
        context.Devices.Remove(SecondDevice);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}
