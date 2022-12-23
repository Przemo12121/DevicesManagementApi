using T_Database.SearchOptions.UserOptions;

namespace T_Database.T_UsersRepository;

public class T_FindEmployees : LocalAuthDatabaseTest
{
    public T_FindEmployees() : base("FindEmployees") { }

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
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 3",
            Password = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
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
    public void FindEmployees_ReturnsThreeRecords()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entities = repo.FindEmployees(new LimitableSearchOptions(100));

        entities.Should().HaveCount(3);
    }

    [Fact]
    public void FindEmployees_ReturnsOnlyEmployeesRecords()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entities = repo.FindEmployees(new LimitableSearchOptions(100));

        entities.Should().AllSatisfy(
            e => e.AccessLevel.Value.Should().Be(AccessLevels.Employee)
            );
    }

    [Fact]
    public void FindEmployees_WithLimitOfTwo_ReturnsTwoRecords()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        int limit = 2;

        var entities = repo.FindEmployees(new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void FindEmployees_WithOffsetOfTwo_ReturnsOneRecord()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        int offset = 2;

        var entities = repo.FindEmployees(new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void FindEmployees_WithOffsetOfOne_ReturnsSecondAndThirdEmployee()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        int offset = 1;

        var entities = repo.FindEmployees(new OffsetableSearchOptions(offset));

        entities.Should().AllSatisfy(
            e => e.EmployeeId.Should().BeOneOf(new[] { "some id 4", "some id 5" })
            );
    }

    [Fact]
    public void FindEmployees_WithOrderNameASC_ReturnsEmployeesOrderByNameASC()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entities = repo.FindEmployees(new OrderableByNameAscSearchOptions());

        entities[0].Name.Should().Be("dummy user");
        entities[1].Name.Should().Be("dummy user 4");
        entities[2].Name.Should().Be("dummy user 5");
    }

    [Fact]
    public void FindEmployees_WithOrderNameDESC_ReturnsEmployeesOrderByNameDESC()
    {
        using var context = new LocalAuthContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entities = repo.FindEmployees(new OrderableByNameDescSearchOptions());

        entities[0].Name.Should().Be("dummy user 5");
        entities[1].Name.Should().Be("dummy user 4");
        entities[2].Name.Should().Be("dummy user");
    }
}