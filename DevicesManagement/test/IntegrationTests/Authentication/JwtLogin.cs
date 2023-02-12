namespace IntegrationTests.Authentication;

[Collection("IntegrationTests")]
public partial class JwtLogin
{
    [Fact]
    public async Task JwtLogin_NonExisitingEmployeeId_401()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(WrongLoginRequest));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task JwtLogin_NonExisitingEmployeeId_DoesNotReturnAuthorizationHeader()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(WrongLoginRequest));

        response.Headers.NonValidated.Should().NotContain(header => header.Key == "Authorization");
    }

    [Fact]
    public async Task JwtLogin_ExistingEmployeeIdWithWrongPassword_401()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(WrongPasswordRequest));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task JwtLogin_ExistingEmployeeIdWithWrongPassword_DoesNotReturnAuthorizationHeader()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(WrongPasswordRequest));

        response.Headers.NonValidated.Should().NotContain(header => header.Key == "Authorization");
    }

    [Fact]
    public async Task JwtLogin_ValidCredentials_200()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(ValidRequest));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task JwtLogin_ValidCredentials_ReturnsAuhtorizationHeader()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(ValidRequest));

        response.Headers.NonValidated.Should().Contain(header => header.Key == "Authorization");
    }

    [Fact]
    public async Task JwtLogin_ValidCredentials_ReturnsJwtInAuhtorizationHeader()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(ValidRequest));

        var authHeader = response.Headers.NonValidated
            .Where(header => header.Key == "Authorization")
            .First()
            .Value
            .ToString();

        new JwtSecurityTokenHandler()
            .Invoking(handler => handler.ReadJwtToken(authHeader))
            .Should()
            .NotThrow();
    }

    [Fact]
    public async Task JwtLogin_ValidCredentials_ReturnedJwtContainsUniqueNameClaim()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(ValidRequest));

        var authHeader = response.Headers.NonValidated
            .Where(header => header.Key == "Authorization")
            .First()
            .Value
            .ToString();

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(authHeader);

        jwt.Claims.Should().Contain(claim => claim.Type.Equals("unique_name"));
    }

    [Fact]
    public async Task JwtLogin_ValidCredentials_ReturnedJwtContainsRoleClaim()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(ValidRequest));

        var authHeader = response.Headers.NonValidated
            .Where(header => header.Key == "Authorization")
            .First()
            .Value
            .ToString();

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(authHeader);

        jwt.Claims.Should().Contain(claim => claim.Type.Equals("role"));
    }

    [Fact]
    public async Task JwtLogin_ValidCredentials_NameClaimIsAuthenticatedUserEmployeeId()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(ValidRequest));

        var authHeader = response.Headers.NonValidated
            .Where(header => header.Key == "Authorization")
            .First()
            .Value
            .ToString();

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(authHeader);

        var uniqeName = jwt.Claims.Where(claim => claim.Type.Equals("unique_name")).First();

        uniqeName.Value.Should().Be(RequestingUser.EmployeeId);
    }

    [Fact]
    public async Task JwtLogin_ValidCredentials_RoleClaimIsAuthenticatedUserRole()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(ValidRequest));

        var authHeader = response.Headers.NonValidated
            .Where(header => header.Key == "Authorization")
            .First()
            .Value
            .ToString();

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(authHeader);

        var role = jwt.Claims.Where(claim => claim.Type.Equals("role")).First();

        role.Value.Should().Be(RequestingUser.AccessLevel.Value.ToString());
    }
}