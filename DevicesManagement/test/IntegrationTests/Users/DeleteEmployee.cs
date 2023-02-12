namespace IntegrationTests.Users;

[Collection("IntegrationTests")]
public partial class DeleteEmployee
{
    [Fact]
    public async void DeleteEmployee_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyUser));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void DeleteEmployee_ValidRequest_RemovesRequestedUserFromDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyUser));

        using var context = new LocalAuthStorageContext();
        context.Users.Should().NotContain(DummyUser);
    }

    [Fact]
    public async void DeleteEmployee_ValidRequest_DoesNotRemovesOtherUserFromDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);

        var response = await HttpClient.DeleteAsync(Route(DummyUser));

        using var context = new LocalAuthStorageContext();
        context.Users.Should().Contain(OtherUser);
    }

    [Fact]
    public async void DeleteEmployee_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.DeleteAsync(Route(DummyUser));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void DeleteEmployee_RequestWithoutToken_DoesNotRemoveUser()
    {
        var response = await HttpClient.DeleteAsync(Route(DummyUser));

        using var context = new LocalAuthStorageContext();
        context.Users.Should().Contain(DummyUser);
    }

    [Fact]
    public async void DeleteEmployee_NonExistingEntity_ResponsesWith404()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", DummyUserJwt);

        var response = await HttpClient.DeleteAsync(Route(new User() { Id = Guid.NewGuid() }));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
