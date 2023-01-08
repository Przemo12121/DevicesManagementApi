using Database.Contexts;
using Microsoft.AspNetCore.Identity;

namespace IntegrationTests.Authentication;

public class Setup : IDisposable
{ 
    public User DummyUser { get; init; }
    public AccessLevel DummyAccessLevel { get; init; }
    private LocalAuthStorageContext Context { get; init; }

    public Setup()
    {
        // Seed
        Context = new LocalAuthStorageContext();
        DummyAccessLevel = new AccessLevel()
        {
            Value = Database.Models.Enums.AccessLevels.Employee,
            Id = Guid.NewGuid(),
            Description = "dummy"
        };
        DummyUser = new User()
        {
            Name = "Dummy user",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            EmployeeId = "xyzw87654321",
            Enabled = true,
            Id = Guid.NewGuid(),
            AccessLevel = DummyAccessLevel
        };
        DummyUser.PasswordHashed = new PasswordHasher<User>().HashPassword(DummyUser, "dummyPWD123");
        Context.AccessLevels.Add(DummyAccessLevel);
        Context.Users.Add(DummyUser);
        Context.SaveChanges();
    }

    public void Dispose()
    {
        Context.Users.Remove(DummyUser);
        Context.AccessLevels.Remove(DummyAccessLevel);
        Context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}