namespace IntegrationTests.Users;

public partial class RegisterDevice : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetup>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetup _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    string Route(IDatabaseModel entity) => $"api/users/{entity.Id}/devices";

    static RegisterDeviceRequest DummyRequest { get; } = new()
    {
        Name = "Dummy test register device",
        Address = "127.0.0.1:5060"
    };

    public RegisterDevice(WebApplicationFactory<Program> factory, BaseSetup setupFixture)
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