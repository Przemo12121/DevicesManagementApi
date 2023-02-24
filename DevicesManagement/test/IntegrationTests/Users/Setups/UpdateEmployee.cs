namespace IntegrationTests.Users;

public partial class Update : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetupFixture>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetupFixture _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    User OtherUser { get; init; }
    User DummyUser { get; init; }

    public Update(WebApplicationFactory<Program> factory, BaseSetupFixture setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        RequestingUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        DummyUser = new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = "abab12121212",
            Name = "dummy user",
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-10),
            AccessLevelId = _setupFixture.EmployeeAccessLevel.Id,
            PasswordHashed = "not important"
        };

        OtherUser = new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = "cdcd23232323",
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