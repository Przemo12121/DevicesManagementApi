namespace T_Database.T_UsersRepository;

[Collection("RepositoriesTests")]
public partial class T_FindByEmployeId
{
    [Fact]
    public async void FindByEmployeId_ExistingEid_ReturnsEmployeeWithThatEID()
    {
        var entity = await Repository.FindByEmployeeIdAsync("some id 3");

        entity.Should().BeEquivalentTo(SearchedUser);
    }

    [Fact]
    public async void FindByEmployeId_NonexistingEid_ReturnsNull()
    {
        var entity = await Repository.FindByEmployeeIdAsync("non existing eid");

        entity.Should().Be(null);
    }
}

public partial class T_FindByEmployeId : IClassFixture<T_FindByEmployeId_Setup>
{
    private readonly T_FindByEmployeId_Setup _setupFixture;
    User SearchedUser { get; init; }
    UsersRepository Repository { get; init; }

    public T_FindByEmployeId(T_FindByEmployeId_Setup setupFixture) 
    {
        _setupFixture = setupFixture;

        SearchedUser = setupFixture.SearchedUser;
        Repository = new UsersRepository(setupFixture.Context);
    }
}

public class T_FindByEmployeId_Setup : LocalAuthDatabaseTest
{
    public LocalAuthContextTest Context { get; init; }
    public User SearchedUser { get; } = new ()
    {
        CreatedDate = DateTime.Now,
        Name = "dummy user 3",
        UpdatedDate = DateTime.Now,
        Id = Guid.NewGuid(),
        EmployeeId = "some id 3",
        PasswordHashed = "password",
        AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
    };

    public T_FindByEmployeId_Setup() : base("FindEmployeeId") 
    {
        Context = new LocalAuthContextTest("FindEmployeeId");
        Seed(Context);
    }

    private void Seed(LocalAuthContextTest context)
    {
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 2",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
        context.Users.Add(SearchedUser);
        context.SaveChanges();
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 4",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 4",
            PasswordHashed = "password",
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
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.SaveChanges();
    }

}