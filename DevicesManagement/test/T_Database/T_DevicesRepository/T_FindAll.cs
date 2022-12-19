using T_Database.T_DevicesRepository.SearchOptions;

namespace T_Database.T_DevicesRepository;

public class T_FindAll : DeviceMenagementDatabaseTest
{
    public T_FindAll() : base("DevicesReopository.FindAll") { }

    private void Seed(DeviceManagementContextTest context)
    {
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now.AddDays(-10),
            Name = "dummy device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id",
            Address = "some address",
            CommandHistory = new List<CommandHistory>(),
            Commands = new List<Command>(),
            MessageHistory = new List<Message>()
        });
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now.AddDays(-5),
            Name = "dummy device 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 2",
            Address = "some address 2",
            CommandHistory = new List<CommandHistory>(),
            Commands = new List<Command>(),
            MessageHistory = new List<Message>()
        });
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 3",
            Address = "some address 3",
            CommandHistory = new List<CommandHistory>(),
            Commands = new List<Command>(),
            MessageHistory = new List<Message>()
        });
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 4",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 2",
            Address = "some address 4",
            CommandHistory = new List<CommandHistory>(),
            Commands = new List<Command>(),
            MessageHistory = new List<Message>()
        });
        context.SaveChanges();
    }


    [Fact]
    public void FindAll_ReturnsThreeRecords()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAll(new LimitableSearchOptions(100));

        entities.Should().HaveCount(4);
    }

    [Fact]
    public void FindAll_WithLimitOfTwo_ReturnsTwoRecords()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int limit = 2;

        var entities = repo.FindAll(new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void FindAll_WithOffsetOfTwo_ReturnsOneRecord()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int offset = 2;

        var entities = repo.FindAll(new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(2);
    }

    [Fact]
    public void FindAll_WithOffsetOfTwo_ReturnsThirdAndFourthEmployee()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int offset = 2;

        var entities = repo.FindAll(new OffsetableSearchOptions(offset));

        entities.Should().AllSatisfy(
            e => e.Name.Should().BeOneOf(new[] { "dummy device 3", "dummy device 4" })
            );
    }

    [Fact]
    public void FindAll_WithOrderNameASC_ReturnsEmployeesOrderByNameASC()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAll(new OrderableByNameAscSearchOptions());

        entities[0].Name.Should().Be("dummy device");
        entities[1].Name.Should().Be("dummy device 2");
        entities[2].Name.Should().Be("dummy device 3");
        entities[3].Name.Should().Be("dummy device 4");
    }

    [Fact]
    public void FindAll_WithOrderNameDESC_ReturnsEmployeesOrderByNameDESC()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAll(new OrderableByNameDescSearchOptions());

        entities[3].Name.Should().Be("dummy device");
        entities[2].Name.Should().Be("dummy device 2");
        entities[1].Name.Should().Be("dummy device 3");
        entities[0].Name.Should().Be("dummy device 4");
    }
}