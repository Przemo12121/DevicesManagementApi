namespace IntegrationTests.Devices;

public partial class Update
{
    [Fact]
    public async void Update_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateDeviceRequest request = new() 
        {
            Address = "255.255.255.255:255",
            Name = "New Device name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void Update_ValidRequest_UpdatesRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateDeviceRequest request = new()
        {
            Address = "255.255.255.255:255",
            Name = "New Device name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        using var context = new DevicesManagementContext();
        var device = context.Devices.Where(c => c.Equals(DummyDevice)).First();
        device.Name.Should().Be("New Device name");
        device.Address.Should().Be("255.255.255.255:255");
    }

    [Fact]
    public async void Update_ValidRequest_DoesNotUpdatesoTHERRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateDeviceRequest request = new()
        {
            Address = "255.255.255.255:255",
            Name = "New Device name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        using var context = new DevicesManagementContext();
        var device = context.Devices.Where(c => c.Equals(DummyDevice)).First();
        device.EmployeeId.Should().Be(DummyUser.EmployeeId);
    }

    [Fact]
    public async void Update_ValidRequest_UpdatesUpdatedDateTimestampRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateDeviceRequest request = new()
        {
            Address = "255.255.255.255:255",
            Name = "New Device name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        using var context = new DevicesManagementContext();
        var device = context.Devices.Where(c => c.Equals(DummyDevice)).First();
        device.UpdatedDate.Date.Should().Be(DateTime.UtcNow.Date);
        device.UpdatedDate.Hour.Should().Be(DateTime.UtcNow.Hour);
        device.UpdatedDate.Minute.Should().Be(DateTime.UtcNow.Minute);
    }

    [Fact]
    public async void Update_ValidRequest_DoesNotUpdateOtherDeviceRequestedAttributes()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateDeviceRequest request = new()
        {
            Address = "255.255.255.255:255",
            Name = "New Device name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        using var context = new DevicesManagementContext();
        var device = context.Devices.Where(c => c.Equals(OtherDevice)).First();
        device.Name.Should().Be("other device");
        device.Address.Should().Be("127.0.0.1:3010");
    }

    [Fact]
    public async void Update_RequestWithoutToken_ResponsesWith401()
    {
        UpdateDeviceRequest request = new()
        {
            Address = "255.255.255.255:255",
            Name = "New Device name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void Update_RequestWithoutToken_DoesNotUpdateDevice()
    {
        UpdateDeviceRequest request = new()
        {
            Address = "255.255.255.255:255",
            Name = "New Device name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        using var context = new DevicesManagementContext();
        var device = context.Devices.Where(c => c.Equals(DummyDevice)).First();
        device.Name.Should().Be("dummy device");
        device.Address.Should().Be("127.0.0.1:1010");
    }

    [Fact]
    public async void Update_NonExistingEntity_ResponsesWith404()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateDeviceRequest request = new()
        {
            Address = "255.255.255.255:255",
            Name = "New Device name"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(new Device() { Id = Guid.NewGuid() }), body);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async void Update_BadRequest_ResponsesWith400()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);
        UpdateDeviceRequest request = new()
        {
            Address = "255.255.255.255:255",
            Name = Enumerable.Range(0, 1000).Select(i => "c").Aggregate((a, b) => a + b)
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void Update_BadRequest_DoesNotUpdateDevice()
    {
        UpdateDeviceRequest request = new()
        {
            Name = Enumerable.Range(0, 100).Select(i => "c").Aggregate((a, b) => a + b),
            Address = "255.255.255.255:255"
        };
        var body = JsonContent.Create(request);

        var response = await HttpClient.PatchAsync(Route(DummyDevice), body);

        using var context = new DevicesManagementContext();
        var device = context.Devices.Where(c => c.Equals(DummyDevice)).First();
        device.Name.Should().Be("dummy device");
        device.Address.Should().Be("127.0.0.1:1010");
    }
}
