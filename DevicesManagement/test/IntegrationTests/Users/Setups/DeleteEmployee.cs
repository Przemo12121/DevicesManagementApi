namespace IntegrationTests.Users;

public partial class Delete : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetupFixture>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetupFixture _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string DummyUserJwt { get; init; }

    User OtherUser { get; init; }
    User DummyUser { get; init; }

    public Delete(WebApplicationFactory<Program> factory, BaseSetupFixture setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        DummyUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        DummyUser = new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = "xyxy56565656",
            Name = "dummy user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            AccessLevelId = _setupFixture.EmployeeAccessLevel.Id,
            PasswordHashed = "not important"
        };

        OtherUser = new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = "stst78787878",
            Name = "other user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            AccessLevelId = _setupFixture.EmployeeAccessLevel.Id,
            PasswordHashed = "not important"
        };

        using var context = new LocalAuthStorageContext();
        context.Users.AddRange(new[] { DummyUser, OtherUser });
        context.SaveChanges();
    }

    private string Route(IDatabaseModel entity) => $"/api/users/{entity.Id}";

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.RemoveRange(
            context.Users.Where(d => new[] { DummyUser, OtherUser }.Contains(d))
        );
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}