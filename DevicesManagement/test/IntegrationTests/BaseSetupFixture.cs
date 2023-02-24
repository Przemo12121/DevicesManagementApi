public class BaseSetupFixture : IDisposable
{
    public User RequestingUser { get; init; }
    public AccessLevel AdminAccessLevel { get; init; }
    public AccessLevel EmployeeAccessLevel { get; init; }

    public BaseSetupFixture()
    {
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

        using var context = new LocalAuthStorageContext();
        context.AccessLevels.Add(AdminAccessLevel);
        context.AccessLevels.Add(EmployeeAccessLevel);
        context.Users.Add(RequestingUser);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.Remove(RequestingUser);
        context.AccessLevels.Remove(AdminAccessLevel);
        context.AccessLevels.Remove(EmployeeAccessLevel);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}