namespace IntegrationTests.Commands;

[Collection("IntegrationTests")]
public partial class Update
{
    [Fact]
    public async void Update_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateCommandRequest request = new() 
        {
            Body = "New body from request",
            Name = "New command name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void Update_ValidRequest_UpdatesRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = "New command name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        using var context = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        var command = context.Commands.Where(c => c.Equals(DummyCommand)).First();
        command.Name.Should().Be("New command name");
        command.Body.Should().Be("New body from request");
    }

    [Fact]
    public async void Update_ValidRequest_DoesNotUpdatesoTHERRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = "New command name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        using var context = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        var command = context.Commands.Where(c => c.Equals(DummyCommand)).First();
        command.Description.Should().Be("dummy description");
    }

    [Fact]
    public async void Update_ValidRequest_UpdatesUpdatedDateTimestampRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = "New command name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        using var context = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        var command = context.Commands.Where(c => c.Equals(DummyCommand)).First();
        command.UpdatedDate.Date.Should().Be(DateTime.UtcNow.Date);
        command.UpdatedDate.Hour.Should().Be(DateTime.UtcNow.Hour);
        command.UpdatedDate.Minute.Should().Be(DateTime.UtcNow.Minute);
    }

    [Fact]
    public async void Update_ValidRequest_DoesNotUpdateOtherCommandRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = "New command name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        using var context = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        var command = context.Commands.Where(c => c.Equals(OtherCommand)).First();
        command.Name.Should().Be("other command");
        command.Body.Should().Be("other body");
    }

    [Fact]
    public async void Update_RequestWithoutToken_ResponsesWith401()
    {
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = "New command name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void Update_RequestWithoutToken_DoesNotUpdateCommand()
    {
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = "New command name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        using var context = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        var command = context.Commands.Where(c => c.Equals(DummyCommand)).First();
        command.Name.Should().Be("dummy command");
        command.Body.Should().Be("dummy body");
    }

    [Fact]
    public async void Update_NonExistingEntity_ResponsesWith404()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = "New command name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(new Command() { Id = Guid.NewGuid() }), body);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void Update_BadRequest_ResponsesWith400()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = Enumerable.Range(0, 100).Select(i => "c").Aggregate((a, b) => a + b)
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void Update_BadRequest_DoesNotUpdateCommand()
    {
        UpdateCommandRequest request = new()
        {
            Body = "New body from request",
            Name = Enumerable.Range(0, 100).Select(i => "c").Aggregate((a, b) => a + b)
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyCommand), body);

        using var context = new DevicesManagementContext(
            _factory.Services.GetRequiredService<DbContextOptions<DevicesManagementContext>>()
        );
        var command = context.Commands.Where(c => c.Equals(DummyCommand)).First();
        command.Name.Should().Be("dummy command");
        command.Body.Should().Be("dummy body");
    }
}
