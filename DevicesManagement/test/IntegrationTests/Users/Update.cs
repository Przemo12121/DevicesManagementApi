namespace IntegrationTests.Users;

[Collection("IntegrationTests")]
public partial class Update
{
    [Fact]
    public async void Update_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new() 
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void Update_ValidRequest_UpdatesRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthContext(
            _factory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.Name.Should().Be("New employee name");
    }

    [Fact]
    public async void Update_ValidRequest_DoesNotUpdatesoTHERRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthContext(
            _factory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.EmployeeId.Should().Be(DummyUser.EmployeeId);
    }

    [Fact]
    public async void Update_ValidRequest_UpdatesUpdatedDateTimestampRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthContext(
            _factory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.UpdatedDate.Date.Should().Be(DateTime.UtcNow.Date);
        User.UpdatedDate.Hour.Should().Be(DateTime.UtcNow.Hour);
        User.UpdatedDate.Minute.Should().Be(DateTime.UtcNow.Minute);
    }

    [Fact]
    public async void Update_ValidRequest_DoesNotUpdateOtherUserRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthContext(
            _factory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        var User = context.Users.Where(c => c.Equals(OtherUser)).First();
        User.Name.Should().Be("other user");
    }

    [Fact]
    public async void Update_RequestWithoutToken_ResponsesWith401()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "New User name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void Update_RequestWithoutToken_DoesNotUpdateUser()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "New User name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthContext(
            _factory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.Name.Should().Be("dummy user");
    }

    [Fact]
    public async void Update_NonExistingEntity_ResponsesWith404()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New User name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(new User() { Id = Guid.NewGuid() }), body);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void Update_BadRequest_ResponsesWith400()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = Enumerable.Range(0, 1000).Select(i => "c").Aggregate((a, b) => a + b)
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void Update_BadRequest_DoesNotUpdateUser()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = Enumerable.Range(0, 100).Select(i => "c").Aggregate((a, b) => a + b),
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthContext(
            _factory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.Name.Should().Be("dummy user");
    }
}
