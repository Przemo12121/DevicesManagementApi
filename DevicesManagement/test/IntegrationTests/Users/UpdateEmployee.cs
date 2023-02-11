namespace IntegrationTests.Users;

public partial class UpdateEmployee
{
    [Fact]
    public async void YpdateEmployeeValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new() 
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);
        var x = Route(DummyUser);
        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void YpdateEmployeeValidRequest_UpdatesRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthStorageContext();
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.Name.Should().Be("New employee name");
    }

    [Fact]
    public async void YpdateEmployeeValidRequest_DoesNotUpdatesoTHERRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthStorageContext();
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.EmployeeId.Should().Be(DummyUser.EmployeeId);
    }

    [Fact]
    public async void YpdateEmployeeValidRequest_UpdatesUpdatedDateTimestampRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthStorageContext();
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.UpdatedDate.Date.Should().Be(DateTime.UtcNow.Date);
        User.UpdatedDate.Hour.Should().Be(DateTime.UtcNow.Hour);
        User.UpdatedDate.Minute.Should().Be(DateTime.UtcNow.Minute);
    }

    [Fact]
    public async void YpdateEmployeeValidRequest_DoesNotUpdateOtherUserRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);
        UpdateEmployeeRequest request = new()
        {
            Name = "New employee name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthStorageContext();
        var User = context.Users.Where(c => c.Equals(OtherUser)).First();
        User.Name.Should().Be("other user");
    }

    [Fact]
    public async void YpdateEmployeeRequestWithoutToken_ResponsesWith401()
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
    public async void YpdateEmployeeRequestWithoutToken_DoesNotUpdateUser()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = "New User name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthStorageContext();
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.Name.Should().Be("dummy user");
    }

    [Fact]
    public async void YpdateEmployeeNonExistingEntity_ResponsesWith404()
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
    public async void YpdateEmployeeBadRequest_ResponsesWith400()
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
    public async void YpdateEmployeeBadRequest_DoesNotUpdateUser()
    {
        UpdateEmployeeRequest request = new()
        {
            Name = Enumerable.Range(0, 100).Select(i => "c").Aggregate((a, b) => a + b),
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyUser), body);

        using var context = new LocalAuthStorageContext();
        var User = context.Users.Where(c => c.Equals(DummyUser)).First();
        User.Name.Should().Be("dummy user");
    }
}
