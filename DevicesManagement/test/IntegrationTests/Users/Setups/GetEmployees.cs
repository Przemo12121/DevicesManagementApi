namespace IntegrationTests.Users;

public partial class GetEmployees : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetup>, IDisposable
{
    WebApplicationFactory<Program> _factory;
    BaseSetup _setupFixture;

    List<User> DummyUsers { get; init; }
    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    string Route { get; } = "api/users/employees";

    public GetEmployees(WebApplicationFactory<Program> webApplicationFactory, BaseSetup setupFixture) 
    {
        _setupFixture = setupFixture;
        _factory = webApplicationFactory;

        HttpClient = _factory.CreateClient();
        RequestingUser = _setupFixture.RequestingUser;
        RequestingUserJwt = _factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        DummyUsers = new()
        {
            // first by eid, last by name
            new()
            {
                AccessLevelId = setupFixture.EmployeeAccessLevel.Id,
                EmployeeId = "aaaa12345678",
                Name = "Zzz-name",
                Enabled = true,
                Id = Guid.NewGuid(),
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                PasswordHashed = "not important"
            },
            // second by eid, second by name
            new()
            {
                AccessLevelId = setupFixture.EmployeeAccessLevel.Id,
                EmployeeId = "bbbb12345678",
                Name = "Mmm-name",
                Enabled = true,
                Id = Guid.NewGuid(),
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                PasswordHashed = "not important"
            },
            // third by eid, first by name
            new()
            {
                AccessLevelId = setupFixture.EmployeeAccessLevel.Id,
                EmployeeId = "cccc12345678",
                Name = "Aaa-name",
                Enabled = true,
                Id = Guid.NewGuid(),
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                PasswordHashed = "not important"
            },
        };

        using var context = new LocalAuthStorageContext();
        context.Users.AddRange(DummyUsers);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.RemoveRange(DummyUsers);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}
