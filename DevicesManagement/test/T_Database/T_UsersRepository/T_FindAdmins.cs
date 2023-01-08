using T_Database.SearchOptions.UserOptions;

namespace T_Database.T_UsersRepository;

public partial class T_FindAdmins
{
    [Fact]
    public void FindAllAdmins_ReturnsThreeRecords()
    {
        var entities = Repository.FindAdmins(new LimitableSearchOptions(100));

        entities.Should().HaveCount(3);
    }

    [Fact]
    public void FindAllAdmins_ReturnsOnlyAdminsRecords()
    {
        var entities = Repository.FindAdmins(new LimitableSearchOptions(100));

        entities.Should().AllSatisfy(
            e => e.AccessLevel.Value.Should().Be(AccessLevels.Admin)
        );
    }

    [Fact]
    public void FindAllAdmins_WithLimitOfTwo_ReturnsTwoRecords()
    {
        int limit = 2;
        var entities = Repository.FindAdmins(new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void FindAllAdmins_WithOffsetOfTwo_ReturnsOneRecord()
    {
        int offset = 2;
        var entities = Repository.FindAdmins(new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void FindAllAdmins_WithOffsetOfOne_ReturnsSecondAndThirdAdmin()
    {
        int offset = 1;
        var entities = Repository.FindAdmins(new OffsetableSearchOptions(offset));

        entities.Should().AllSatisfy(
            e => e.EmployeeId.Should().BeOneOf(new[] { "some id 4", "some id 5" })
        );
    }

    [Fact]
    public void FindAllAdmins_WithOrderNameASC_ReturnsAdminsOrderByNameASC()
    {
        var entities = Repository.FindAdmins(new OrderableByNameAscSearchOptions());
            
        entities[0].Name.Should().Be("dummy user");
        entities[1].Name.Should().Be("dummy user 4");
        entities[2].Name.Should().Be("dummy user 5");
    }

    [Fact]
    public void FindAllAdmins_WithOrderNameDESC_ReturnsAdminsOrderByNameDESC()
    {
        var entities = Repository.FindAdmins(new OrderableByNameDescSearchOptions());

        entities[0].Name.Should().Be("dummy user 5");
        entities[1].Name.Should().Be("dummy user 4");
        entities[2].Name.Should().Be("dummy user");
    }
}

public partial class T_FindAdmins : IClassFixture<T_FindAdmins_Setup>
{
    private readonly T_FindAdmins_Setup _setupFixture;
    UsersRepository Repository { get; init; }
    public T_FindAdmins(T_FindAdmins_Setup setupFixutre) 
    {
        _setupFixture = setupFixutre;
        Repository = new UsersRepository(setupFixutre.Context);
    }
}

public class T_FindAdmins_Setup : LocalAuthDatabaseTest
{
    public LocalAuthContextTest Context { get; init; }
    public T_FindAdmins_Setup() : base("FindAdmins") 
    {
        Context = new LocalAuthContextTest("FindAdmins");
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
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 2",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 3",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.SaveChanges();
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 4",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 4",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
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
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
        context.SaveChanges();
    }
}