using T_Database.SearchOptions.UserOptions;

namespace T_Database.T_UsersRepository;

public class T_FindAdmins : LocalAuthDatabaseTest
{
    public T_FindAdmins() : base("FindAdmins") { }

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
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 2",
            Password = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 3",
            Password = "password",
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
            Password = "password",
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
            Password = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
        context.SaveChanges();
    }


    [Fact]
    public void FindAllAdmins_ReturnsThreeRecords()
    {
        using var context = new LocalAuthContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entities = repo.FindAdmins(new LimitableSearchOptions(100));

        entities.Should().HaveCount(3);
    }

    [Fact]
    public void FindAllAdmins_ReturnsOnlyAdminsRecords()
    {
        using var context = new LocalAuthContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entities = repo.FindAdmins(new LimitableSearchOptions(100));

        entities.Should().AllSatisfy(
            e => e.AccessLevel.Value.Should().Be(AccessLevels.Admin)
            );
    }

    [Fact]
    public void FindAllAdmins_WithLimitOfTwo_ReturnsTwoRecords()
    {
        using var context = new LocalAuthContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        int limit = 2;

        var entities = repo.FindAdmins(new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void FindAllAdmins_WithOffsetOfTwo_ReturnsOneRecord()
    {
        using var context = new LocalAuthContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        int offset = 2;

        var entities = repo.FindAdmins(new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void FindAllAdmins_WithOffsetOfOne_ReturnsSecondAndThirdAdmin()
    {
        using var context = new LocalAuthContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        int offset = 1;

        var entities = repo.FindAdmins(new OffsetableSearchOptions(offset));

        entities.Should().AllSatisfy(
            e => e.EmployeeId.Should().BeOneOf(new[] { "some id 4", "some id 5" })
            );
    }

    [Fact]
    public void FindAllAdmins_WithOrderNameASC_ReturnsAdminsOrderByNameASC()
    {
        using var context = new LocalAuthContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entities = repo.FindAdmins(new OrderableByNameAscSearchOptions());

        entities[0].Name.Should().Be("dummy user");
        entities[1].Name.Should().Be("dummy user 4");
        entities[2].Name.Should().Be("dummy user 5");
    }

    [Fact]
    public void FindAllAdmins_WithOrderNameDESC_ReturnsAdminsOrderByNameDESC()
    {
        using var context = new LocalAuthContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new UsersRepository(context);

        var entities = repo.FindAdmins(new OrderableByNameDescSearchOptions());

        entities[0].Name.Should().Be("dummy user 5");
        entities[1].Name.Should().Be("dummy user 4");
        entities[2].Name.Should().Be("dummy user");
    }
}