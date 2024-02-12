namespace IntegrationTests.Authentication;

public partial class JwtLogin : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetupFixture>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetupFixture _setupFixture;

    private HttpClient HttpClient { get; init; }
    private User RequestingUser { get; init; }

    public JwtLogin(WebApplicationFactory<Program> factory, BaseSetupFixture setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;
        _setupFixture.Init(factory);

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;

        ValidRequest = new()
        {
            Login = "zwxy23654322",
            Password = "dummyPWD123"
        };
        WrongPasswordRequest = new()
        {
            Login = "zwxy23654322",
            Password = "badPWD123"
        };
    }

    private string Route { get; } = "/api/authentication/jwt/login";

    private LoginWithCredentialsRequest ValidRequest { get; init; }

    private LoginWithCredentialsRequest WrongLoginRequest { get; } = new()
    {
        Login = "badw87654321",
        Password = "dummyPWD123"
    };

    private LoginWithCredentialsRequest WrongPasswordRequest { get; init; }

    public void Dispose()
    {
        _setupFixture.Clear();

        GC.SuppressFinalize(this);
    }
}