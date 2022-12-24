using T_Database.SearchOptions.UserOptions;

namespace T_Database.T_UsersRepository;

public class T_FindByEmployeId : LocalAuthDatabaseTest
{
    private readonly User searchedUser = new()
    {
        CreatedDate = DateTime.Now,
        Name = "dummy user 3",
        UpdatedDate = DateTime.Now,
        Id = Guid.NewGuid(),
        EmployeeId = "some id 3",
        Password = "password",
        AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
    };

    public T_FindByEmployeId() : base("FindEmployeeId") { }

    private void Seed(LocalAuthContextTest context)
    {
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id",
            Password = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 2",
            Password = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
        context.Users.Add(searchedUser);
        context.SaveChanges();
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 4",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 4",
            Password = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.SaveChanges();
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 5",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 5",
            Password = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.SaveChanges();
    }


    [Fact]
    public void FindByEmployeId_ExistingEid_ReturnsEmployeeWithThatEID()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entity = repo.FindByEmployeeId("some id 3");

        entity.Should().BeEquivalentTo(searchedUser);
    }

    [Fact]
    public void FindByEmployeId_NonexistingEid_ReturnsNull()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entity = repo.FindByEmployeeId("non existing eid");

        entity.Should().Be(null);
    }
}