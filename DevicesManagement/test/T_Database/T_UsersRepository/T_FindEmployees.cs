using T_Database.SearchOptions.UserOptions;

namespace T_Database.T_UsersRepository;

[Collection("RepositoriesTests")]
public partial class T_FindEmployees
{
    [Fact]
    public async void FindEmployees_ReturnsThreeRecords()
    {
        var entities = await Repository.FindEmployeesAsync(new LimitableSearchOptions(100));

        entities.Should().HaveCount(3);
    }

    [Fact]
    public async void FindEmployees_ReturnsOnlyEmployeesRecords()
    {
        var entities = await Repository.FindEmployeesAsync(new LimitableSearchOptions(100));

        entities.Should().AllSatisfy(
            e => e.AccessLevel.Value.Should().Be(AccessLevels.Employee)
        );
    }

    [Fact]
    public async void FindEmployees_WithLimitOfTwo_ReturnsTwoRecords()
    {
        int limit = 2;
        var entities = await Repository.FindEmployeesAsync(new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public async void FindEmployees_WithOffsetOfTwo_ReturnsOneRecord()
    {
        int offset = 2;
        var entities = await Repository.FindEmployeesAsync(new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public async void FindEmployees_WithOffsetOfOne_ReturnsSecondAndThirdEmployee()
    {
        int offset = 1;
        var entities = await Repository.FindEmployeesAsync(new OffsetableSearchOptions(offset));

        entities.Should().AllSatisfy(
            e => e.EmployeeId.Should().BeOneOf(new[] { "some id 4", "some id 5" })
        );
    }

    [Fact]
    public async void FindEmployees_WithOrderNameASC_ReturnsEmployeesOrderByNameASC()
    {
        var entities = await Repository.FindEmployeesAsync(new OrderableByNameAscSearchOptions());

        entities[0].Name.Should().Be("a dummy user");
        entities[1].Name.Should().Be("d dummy user 4");
        entities[2].Name.Should().Be("e dummy user 5");
    }

    [Fact]
    public async void FindEmployees_WithOrderNameDESC_ReturnsEmployeesOrderByNameDESC()
    {
        var entities = await Repository.FindEmployeesAsync(new OrderableByNameDescSearchOptions());

        entities[0].Name.Should().Be("e dummy user 5");
        entities[1].Name.Should().Be("d dummy user 4");
        entities[2].Name.Should().Be("a dummy user");
    }
}

public partial class T_FindEmployees : IClassFixture<T_FindEmployees_Setup>
{
    private readonly T_FindEmployees_Setup _setupFixture;
    UsersRepository Repository { get; init; }

    public T_FindEmployees(T_FindEmployees_Setup setupFixture)
    {
        _setupFixture = setupFixture;
        Repository = new UsersRepository(setupFixture.Context);
    }
}

public class T_FindEmployees_Setup : LocalAuthDatabaseTest
{
    public LocalAuthContextTest Context { get; init; }

    public T_FindEmployees_Setup() : base("FindEmployees") 
    {
        Context = new LocalAuthContextTest("FindEmployees");
        Seed(Context);
    }

    private void Seed(LocalAuthContextTest context)
    {
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "a dummy user",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "b dummy user 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 2",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "c dummy user 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 3",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Admin }
        });
        context.SaveChanges();
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "d dummy user 4",
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
            Name = "e dummy user 5",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 5",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = AccessLevels.Employee }
        });
        context.SaveChanges();
    }
}