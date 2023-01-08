using T_Database.SearchOptions.DeviceOptions;

namespace T_Database.T_DevicesRepository;

public partial class T_FindAllByEmployeeId
{
    [Fact]
    public void FindAllByEmployeId_ExistingEmployeeId_ReturnsDevicesWithThatEmployeeId()
    {
        var entities = Repository.FindAllByEmployeeId("some employee id 2", new LimitableSearchOptions(100));

        entities.Should().HaveCount(2);
        entities[0].Should().BeEquivalentTo(SearchedDevice);
        entities[1].Should().BeEquivalentTo(SearchedDevice2);
    }

    [Fact]
    public void FindAllByEmployeId_WithLimitOf1_Returns1Device()
    {
        var entities = Repository.FindAllByEmployeeId("some employee id 2", new LimitableSearchOptions(1));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void FindAllByEmployeId_WithOffsetOf1_ReturnsSecondMatchingDevice()
    {
        var entities = Repository.FindAllByEmployeeId("some employee id 2", new OffsetableSearchOptions(1));

        entities[0].Should().BeEquivalentTo(SearchedDevice2);
    }
    
    [Fact]
    public void FindAllByEmployeId_WithOrderByNameAsc_ReturnsDevicesOrderedByNameAsc()
    {
        var entities = Repository.FindAllByEmployeeId("some employee id 2", new OrderableByNameAscSearchOptions());

        entities[0].Should().BeEquivalentTo(SearchedDevice2);
        entities[1].Should().BeEquivalentTo(SearchedDevice);
    }

    [Fact]
    public void FindAllByEmployeId_WithOrderByNameDesc_ReturnsDevicesOrderedByNameDesc()
    {
        var entities = Repository.FindAllByEmployeeId("some employee id 2", new OrderableByNameDescSearchOptions());

        entities[0].Should().BeEquivalentTo(SearchedDevice);
        entities[1].Should().BeEquivalentTo(SearchedDevice2);
    }

    [Fact]
    public void FindByEmployeId_NonexistingEmployeeId_ReturnsEmptyCollection()
    {
        var entities = Repository.FindAllByEmployeeId("non existind eid", new LimitableSearchOptions(100));

        entities.Should().HaveCount(0);
    }
}

public partial class T_FindAllByEmployeeId : IClassFixture<T_FindAllByEmployeeId_Setup>
{
    private readonly T_FindAllByEmployeeId_Setup _setupFixture;

    Device SearchedDevice { get; init; }
    Device SearchedDevice2 { get; init; }

    DevicesRepository Repository { get; init; }

    public T_FindAllByEmployeeId(T_FindAllByEmployeeId_Setup setupFixture) 
    {
        _setupFixture = setupFixture;
        Repository = new DevicesRepository(setupFixture.Context);
        SearchedDevice = setupFixture.SearchedDevice;
        SearchedDevice2 = setupFixture.SearchedDevice2;
    }
}

public class T_FindAllByEmployeeId_Setup : DeviceMenagementDatabaseTest
{
    public DeviceManagementContextTest Context { get; init; }
    public Device SearchedDevice { get; } = new()
    {
        CreatedDate = DateTime.Now,
        Name = "zz dummy device",
        UpdatedDate = DateTime.Now,
        Id = Guid.Parse("12345678-abcd-1234-abcd-123456abcdef"),
        EmployeeId = "some employee id 2",
        Address = "some address 2",
        Commands = new List<Command>(),
        Messages = new List<Message>()
    };
    public Device SearchedDevice2 { get; } = new()
    {
        CreatedDate = DateTime.Now,
        Name = "aa dummy device",
        UpdatedDate = DateTime.Now,
        Id = Guid.Parse("abcdabcd-abcd-1234-abcd-123456abcdef"),
        EmployeeId = "some employee id 2",
        Address = "some address 4",
        Commands = new List<Command>(),
        Messages = new List<Message>()
    };

    public T_FindAllByEmployeeId_Setup() : base("DevicesRepository.FindAllByEmployeeId")
    {
        Context = new DeviceManagementContextTest("DevicesRepository.FindAllByEmployeeId");

        EnsureClear(Context);
        Seed(Context);
    }

    private void Seed(DeviceManagementContextTest context)
    {
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id",
            Address = "some address",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.Devices.Add(SearchedDevice);
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
        context.Devices.Add(SearchedDevice2);
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 5",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 3",
            Address = "some address 5",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.SaveChanges();
    }
}