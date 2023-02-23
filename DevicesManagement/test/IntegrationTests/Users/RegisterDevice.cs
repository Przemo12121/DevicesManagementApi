namespace IntegrationTests.Users;

[Collection("IntegrationTests")]
public partial class RegisterDevice
{
    [Fact]
    public async void RegisterEmployeeDevice_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(DummyRequest));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void RegisterEmployeeDevice_ValidRequest_CreateNewDeviceInDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        int countBefore;
        using (var context = new DevicesManagementContext())
        {
            countBefore = context.Devices.Count();
        }

        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(DummyRequest));


        using (var context = new DevicesManagementContext())
        {
            context.Devices.Should().HaveCount(countBefore + 1);
        }
    }

    [Fact]
    public async void RegisterEmployeeDevice_ValidRequest_NewlyCreatedDeviceHasRequestedEmployeeEid()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(DummyRequest));

        using var context = new DevicesManagementContext();
        var newDevice = context.Devices.First();
        newDevice.EmployeeId.Should().Be(RequestingUser.EmployeeId);
    }

    [Fact]
    public async void RegisterEmployeeDevice_ValidRequest_NewlyCreatedDeviceHasRequestedName()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(DummyRequest));

        using var context = new DevicesManagementContext();
        var newUser = context.Devices.First();
        newUser.Name.Should().Be(DummyRequest.Name);
    }

    [Fact]
    public async void RegisterEmployeeDevice_ValidRequest_NewlyCreatedUserHasRequestedAddress()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(DummyRequest));

        using var context = new DevicesManagementContext();
        var newUser = context.Devices.First();
        newUser.Address.Should().Be(DummyRequest.Address);
    }

    [Fact]
    public async void RegisterEmployeeDevice_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(DummyRequest));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void RegisterEmployeeDevice_RequestWithoutToken_DoesNotCreateDevice()
    {
        int countBefore;
        using (var context = new DevicesManagementContext())
        {
            countBefore = context.Devices.Count();
        }

        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(DummyRequest));

        using (var context = new DevicesManagementContext())
        {
            context.Devices.Should().HaveCount(countBefore);
        }
    }

    [Fact]
    public async void RegisterEmployeeDevice_BadRequest_ResponsesWith400()
    {
        RegisterDeviceRequest invalidRequest = new()
        {
            Name = "aaa",
            Address = "abcabc"
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(invalidRequest));
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void RegisterEmployeeDevice_BadRequest_DoesNotCreateNewDevice()
    {
        int countBefore;
        using (var context = new DevicesManagementContext())
        {
            countBefore = context.Devices.Count();
        }

        RegisterDeviceRequest invalidRequest = new()
        {
            Name = "aaa",
            Address = "abcabc"
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(RequestingUser), JsonContent.Create(invalidRequest));

        using (var context = new DevicesManagementContext())
        {
            context.Devices.Should().HaveCount(countBefore);
        }
    }
}
