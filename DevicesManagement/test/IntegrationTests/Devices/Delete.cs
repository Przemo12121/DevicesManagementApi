namespace IntegrationTests.Devices;

[Collection("IntegrationTests")]
public partial class Delete
{
    [Fact]
    public async void Delete_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyDevice));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void Delete_ValidRequest_RemovesRequestedDeviceFromDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyDevice));

        using var context = new DevicesManagementContext();
        context.Devices.Should().NotContain(DummyDevice);
    }

    [Fact]
    public async void Delete_ValidRequest_DoesNotRemovesOtherDeviceFromDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyDevice));

        using var context = new DevicesManagementContext();
        context.Devices.Should().Contain(OtherDevice);
    }

    [Fact]
    public async void Delete_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.DeleteAsync(Route(DummyDevice));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void Delete_RequestWithoutToken_DoesNotRemoveDevice()
    {
        var response = await HttpClient.DeleteAsync(Route(DummyDevice));

        using var context = new DevicesManagementContext();
        context.Devices.Should().Contain(DummyDevice);
    }

    [Fact]
    public async void Delete_NonExistingEntity_ResponsesWith404()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.DeleteAsync(Route(new Device() { Id = Guid.NewGuid() }));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
