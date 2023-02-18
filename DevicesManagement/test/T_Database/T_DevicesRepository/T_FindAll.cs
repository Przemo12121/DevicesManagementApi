using T_Database.SearchOptions.DeviceOptions;

namespace T_Database.T_DevicesRepository;

public partial class T_FindAll
{
    [Fact]
    public async void FindAll_ReturnsThreeRecords()
    {
        var entities = await Repository.FindAllAsync(new LimitableSearchOptions(100));

        entities.Should().HaveCount(4);
    }

    [Fact]
    public async void FindAll_WithLimitOfTwo_ReturnsTwoRecords()
    {
        int limit = 2;
        var entities = await Repository.FindAllAsync(new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public async void FindAll_WithOffsetOfTwo_ReturnsOneRecord()
    {
        int offset = 2;
        var entities = await Repository.FindAllAsync(new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(2);
    }

    [Fact]
    public async void FindAll_WithOffsetOfTwo_ReturnsThirdAndFourthEmployee()
    {
        int offset = 2;
        var entities = await Repository.FindAllAsync(new OffsetableSearchOptions(offset));

        entities.Should().AllSatisfy(
            e => e.Name.Should().BeOneOf(new[] { "dummy device 3", "dummy device 4" })
        );
    }

    [Fact]
    public async void FindAll_WithOrderNameASC_ReturnsEmployeesOrderByNameASC()
    {
        var entities = await Repository.FindAllAsync(new OrderableByNameAscSearchOptions());

        entities[0].Name.Should().Be("dummy device");
        entities[1].Name.Should().Be("dummy device 2");
        entities[2].Name.Should().Be("dummy device 3");
        entities[3].Name.Should().Be("dummy device 4");
    }

    [Fact]
    public async void FindAll_WithOrderNameDESC_ReturnsEmployeesOrderByNameDESC()
    {
        var entities = await Repository.FindAllAsync(new OrderableByNameDescSearchOptions());

        entities[3].Name.Should().Be("dummy device");
        entities[2].Name.Should().Be("dummy device 2");
        entities[1].Name.Should().Be("dummy device 3");
        entities[0].Name.Should().Be("dummy device 4");
    }
}

public partial class T_FindAll : IClassFixture<T_FindAll_Setup>
{
    private DevicesRepository Repository { get; init; }
    private readonly T_FindAll_Setup _setupFixture;
    public T_FindAll(T_FindAll_Setup setupFixture) 
    {
        _setupFixture = setupFixture;
        Repository = new DevicesRepository(_setupFixture.Context);
    }
}

public class T_FindAll_Setup : DeviceMenagementDatabaseTest
{
    public DeviceManagementContextTest Context { get; init; }

    public T_FindAll_Setup() : base("DevicesReopository.FindAll")
    {
        Context = new DeviceManagementContextTest("DevicesReopository.FindAll");
        
        EnsureClear(Context);
        Seed(Context);
    }

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
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now.AddDays(-5),
            Name = "dummy device 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 2",
            Address = "some address 2",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 3",
            Address = "some address 3",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 4",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 2",
            Address = "some address 4",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.SaveChanges();
    }
}
