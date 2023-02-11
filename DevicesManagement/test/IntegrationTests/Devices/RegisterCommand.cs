using DevicesManagement.DataTransferObjects.Requests;

namespace IntegrationTests.Devices;

public partial class RegisterCommand
{
    [Fact]
    public async void RegisterCommand_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(DummyRequest));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void RegisterCommand_ValidRequest_CreateNewCommandInDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        int countBefore;
        using (var context = new DevicesManagementContext())
        {
            countBefore = context.Commands.Count();
        }

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(DummyRequest));


        using (var context = new DevicesManagementContext())
        {
            context.Commands.Should().HaveCount(countBefore + 1);
        }
    }
    
    [Fact]
    public async void RegisterCommand_ValidRequest_NewlyCreatedCommandHasRequestedName()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(DummyRequest));

        using var context = new DevicesManagementContext();
        var newCommand = context.Commands.First();
        newCommand.Name.Should().Be(DummyRequest.Name);
    }

    [Fact]
    public async void RegisterCommand_ValidRequest_NewlyCreatedUserHasRequestedBody()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(DummyRequest));

        using var context = new DevicesManagementContext();
        var newCommand = context.Commands.First();
        newCommand.Body.Should().Be(DummyRequest.Body);
    }

    [Fact]
    public async void RegisterCommand_ValidRequest_NewlyCreatedUserHasRequestedDescription()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(DummyRequest));

        using var context = new DevicesManagementContext();
        var newCommand = context.Commands.First();
        newCommand.Description.Should().Be(DummyRequest.Description);
    }

    [Fact]
    public async void RegisterCommand_ValidRequest_NewlyCreatedCommandBelongsToRequestedDevice()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(DummyRequest));

        using var context = new DevicesManagementContext();
        var newCommand = context.Commands.First();
        var device = context.Devices
            .Where(device => device.Id.Equals(DummyDevice.Id))
            .Include(device => device.Commands)
            .First();
        device.Commands.Should().Contain(newCommand);
    }

    [Fact]
    public async void RegisterCommand_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(DummyRequest));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void RegisterCommand_RequestWithoutToken_DoesNotCreateCommand()
    {
        int countBefore;
        using (var context = new DevicesManagementContext())
        {
            countBefore = context.Commands.Count();
        }

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(DummyRequest));

        using (var context = new DevicesManagementContext())
        {
            context.Commands.Should().HaveCount(countBefore);
        }
    }

    [Fact]
    public async void RegisterCommand_BadRequest_ResponsesWith400()
    {
        RegisterCommandRequest invalidRequest = new()
        {
            Name = "aaa",
            //Body = "abcabc"
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(invalidRequest));
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void RegisterCommand_BadRequest_DoesNotCreateNewCommand()
    {
        int countBefore;
        using (var context = new DevicesManagementContext())
        {
            countBefore = context.Commands.Count();
        }

        RegisterCommandRequest invalidRequest = new()
        {
            Name = "aaa",
            //Body = "abcabc"
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route(DummyDevice), JsonContent.Create(invalidRequest));

        using (var context = new DevicesManagementContext())
        {
            context.Commands.Should().HaveCount(countBefore);
        }
    }
}
