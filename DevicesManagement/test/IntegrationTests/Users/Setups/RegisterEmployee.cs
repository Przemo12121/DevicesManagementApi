namespace IntegrationTests.Users;

public partial class RegisterEmployee : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<RegisterEmployeeSetup>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly RegisterEmployeeSetup _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    string Route { get; } = "api/users/employees";

    RegisterEmployeeRequest DummyRequest { get; } = new()
    {
        EmployeeId = "test11100123",
        Name = "Dummy employee",
        Password = "dummyPASSword991"
    };

    User ExistingEmployee { get; init; }

    public RegisterEmployee(WebApplicationFactory<Program> factory, RegisterEmployeeSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        RequestingUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        using var context = new LocalAuthStorageContext();
        ExistingEmployee = new()
        {
            AccessLevelId = _setupFixture.AdminAccessLevel.Id,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            EmployeeId = "aaaa11111111",
            Name = "existing user",
            PasswordHashed = "not important"
        };
        context.Users.Add(ExistingEmployee);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.RemoveRange(
            context.Users.Where(
                d => d.EmployeeId.Equals(DummyRequest.EmployeeId) && d.Name.Equals(DummyRequest.Name)
            )
        );
        context.Users.Remove(ExistingEmployee);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}

public class RegisterEmployeeSetup : IDisposable
{
    public User RequestingUser { get; init; }
    public AccessLevel AdminAccessLevel { get; init; }
    public AccessLevel EmployeeAdminLevel { get; init; }
    public RegisterEmployeeSetup()
    {
        // Seed
        AdminAccessLevel = new AccessLevel()
        {
            Value = Database.Models.Enums.AccessLevels.Admin,
            Id = Guid.NewGuid(),
            Description = "dummy"
        };
        EmployeeAdminLevel = new AccessLevel()
        {
            Value = Database.Models.Enums.AccessLevels.Employee,
            Id = Guid.NewGuid(),
            Description = "dummy employee level"
        };
        RequestingUser = new User()
        {
            Name = "Dummy user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            EmployeeId = "zwxy23654322",
            Enabled = true,
            Id = Guid.NewGuid(),
            AccessLevel = AdminAccessLevel,
        };
        RequestingUser.PasswordHashed = new PasswordHasher<User>().HashPassword(RequestingUser, "dummyPWD123");

        using var context = new LocalAuthStorageContext();
        context.AccessLevels.Add(AdminAccessLevel);
        context.AccessLevels.Add(EmployeeAdminLevel);
        context.Users.Add(RequestingUser);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.Remove(RequestingUser);
        context.AccessLevels.Remove(AdminAccessLevel);
        context.AccessLevels.Remove(EmployeeAdminLevel);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}