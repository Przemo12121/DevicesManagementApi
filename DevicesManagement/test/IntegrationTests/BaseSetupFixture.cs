public class BaseSetupFixture
{
    public User RequestingUser { get; set; }
    public AccessLevel AdminAccessLevel { get; set; }
    public AccessLevel EmployeeAccessLevel { get; set; }

    private WebApplicationFactory<Program> WebApplicationFactory { get; set; }

    public void Init(WebApplicationFactory<Program> webApplicationFactory)
    {
        WebApplicationFactory = webApplicationFactory;

        // Seed
        AdminAccessLevel = new AccessLevel()
        {
            Value = Database.Models.Enums.AccessLevels.Admin,
            Id = Guid.NewGuid(),
            Description = "dummy"
        };
        EmployeeAccessLevel = new AccessLevel()
        {
            Value = Database.Models.Enums.AccessLevels.Employee,
            Id = Guid.NewGuid(),
            Description = "dummy 2"
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

        using var context = new LocalAuthContext(
            WebApplicationFactory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        context.AccessLevels.Add(AdminAccessLevel);
        context.AccessLevels.Add(EmployeeAccessLevel);
        context.Users.Add(RequestingUser);
        context.SaveChanges();
    }

    public void Clear()
    {
        using var context = new LocalAuthContext(
            WebApplicationFactory.Services.GetRequiredService<DbContextOptions<LocalAuthContext>>()
        );
        context.Users.Remove(RequestingUser);
        context.AccessLevels.Remove(AdminAccessLevel);
        context.AccessLevels.Remove(EmployeeAccessLevel);
        context.SaveChanges();
    }
}