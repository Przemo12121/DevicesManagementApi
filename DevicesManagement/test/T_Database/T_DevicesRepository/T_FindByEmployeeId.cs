using T_Database.SearchOptions.DeviceOptions;

namespace T_Database.T_DevicesRepository;

public class T_FindAllByEmployeId : DeviceMenagementDatabaseTest
{
    Device searchedDevice = new ()
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
    Device searchedDevice2 = new ()
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

    public T_FindAllByEmployeId() : base("DevicesRepository.FindAllByEmployeeId") { }

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
        context.Devices.Add(searchedDevice);
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
        context.Devices.Add(searchedDevice2);
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


    [Fact]
    public void FindAllByEmployeId_ExistingEmployeeId_ReturnsDevicesWithThatEmployeeId()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAllByEmployeeId("some employee id 2", new LimitableSearchOptions(100));

        entities.Should().HaveCount(2);
        entities[0].Should().BeEquivalentTo(searchedDevice);
        entities[1].Should().BeEquivalentTo(searchedDevice2);
    }

    [Fact]
    public void FindAllByEmployeId_WithLimitOf1_Returns1Device()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAllByEmployeeId("some employee id 2", new LimitableSearchOptions(1));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void FindAllByEmployeId_WithOffsetOf1_ReturnsSecondMatchingDevice()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAllByEmployeeId("some employee id 2", new OffsetableSearchOptions(1));

        entities[0].Should().BeEquivalentTo(searchedDevice2);
    }
    
    [Fact]
    public void FindAllByEmployeId_WithOrderByNameAsc_ReturnsDevicesOrderedByNameAsc()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAllByEmployeeId("some employee id 2", new OrderableByNameAscSearchOptions());

        entities[0].Should().BeEquivalentTo(searchedDevice2);
        entities[1].Should().BeEquivalentTo(searchedDevice);
    }

    [Fact]
    public void FindAllByEmployeId_WithOrderByNameDesc_ReturnsDevicesOrderedByNameDesc()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAllByEmployeeId("some employee id 2", new OrderableByNameDescSearchOptions());

        entities[0].Should().BeEquivalentTo(searchedDevice);
        entities[1].Should().BeEquivalentTo(searchedDevice2);
    }

    [Fact]
    public void FindByEmployeId_NonexistingEmployeeId_ReturnsEmptyCollection()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.FindAllByEmployeeId("non existind eid", new LimitableSearchOptions(100));

        entities.Should().HaveCount(0);
    }
}