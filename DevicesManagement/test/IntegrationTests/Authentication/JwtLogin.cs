using IntegrationTests.Authentication;
using System.Net.Http.Json;
using System;

[assembly: AssemblyFixture(typeof(SetupFixture))]

namespace IntegrationTests.Authentication;

public class JwtLogin : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly SetupFixture _setupFixture;

    public JwtLogin(WebApplicationFactory<Program> factory, SetupFixture setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;
    }

    [Fact]
    public async Task JwtLogin_NonExisitingEmployeeId_401()
    {
        var client = _factory.CreateClient();

        var request = new LoginWithCredentialsRequest()
        {
            Login = "badx12345678",
            Password = "dummyPWD123"
        };
        var body = JsonContent.Create(request);

        var response = await client.PostAsync("/api/authentication/jwt/login", body);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task JwtLogin_ExistingEmployeeIdWithWrongPassword_401()
    {
        var client = _factory.CreateClient();

        var request = new LoginWithCredentialsRequest()
        {
            Login = "xyzw87654321",
            Password = "passwordDUMMY123"
        };
        var body = JsonContent.Create(request);

        var response = await client.PostAsync("/api/authentication/jwt/login", body);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task JwtLogin_ValidCredentials_200()
    {
        var client = _factory.CreateClient();

        var request = new LoginWithCredentialsRequest()
        {
            Login = "xyzw87654321",
            Password = "dummyPWD123"
        };
        var body = JsonContent.Create(request);

        var response = await client.PostAsync("/api/authentication/jwt/login", body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}