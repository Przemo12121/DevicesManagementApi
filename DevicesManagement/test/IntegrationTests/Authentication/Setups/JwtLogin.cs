namespace IntegrationTests.Authentication;

public partial class JwtLogin : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetup>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetup _setupFixture;

    private HttpClient HttpClient { get; init; }
    private User RequestingUser { get; init; }

    public JwtLogin(WebApplicationFactory<Program> factory, BaseSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

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
}