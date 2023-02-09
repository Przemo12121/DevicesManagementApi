namespace IntegrationTests.Commands;

public partial class Delete
{
    [Fact]
    public async void Delete_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyCommand));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void Delete_ValidRequest_RemovesRequestedCommandFromDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyCommand));

        using var context = new DevicesManagementContext();
        context.Commands.Should().NotContain(DummyCommand);
    }

    [Fact]
    public async void Delete_ValidRequest_DoesNotRemovesOtherCommandFromDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyCommand));

        using var context = new DevicesManagementContext();
        context.Commands.Should().Contain(OtherCommand);
    }

    [Fact]
    public async void Delete_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.DeleteAsync(Route(DummyCommand));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void Delete_RequestWithoutToken_DoesNotRemoveCommand()
    {
        var response = await HttpClient.DeleteAsync(Route(DummyCommand));

        using var context = new DevicesManagementContext();
        context.Commands.Should().Contain(DummyCommand);
    }

    [Fact]
    public async void Delete_NonExistingEntity_ResponsesWith404()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);

        var response = await HttpClient.DeleteAsync(Route(new Command() { Id = Guid.NewGuid() }));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
