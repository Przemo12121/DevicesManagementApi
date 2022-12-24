using T_Database.SearchOptions.DeviceOptions;

namespace T_Database.T_DevicesRepository;

public class T_FindById : DeviceMenagementDatabaseTest
{
    private readonly Device searchedDevice = new()
    {
        CreatedDate = DateTime.Now,
        Name = "dummy device 2",
        UpdatedDate = DateTime.Now,
        Id = Guid.Parse("12345678-abcd-1234-abcd-123456abcdef"),
        EmployeeId = "some employee id 2",
        Address = "some address 2",
        Commands = new List<Command>(),
        Messages = new List<Message>()
    };

public T_FindById() : base("DevicesRepository.FindById") { }

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
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 4",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 3",
            Address = "some address 4",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 5",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 2",
            Address = "some address 5",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.SaveChanges();
    }


    [Fact]
    public void FindByEmployeId_ExistingId_ReturnsDevicesWithThatId()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entity = repo.FindById(Guid.Parse("12345678-abcd-1234-abcd-123456abcdef"));

        entity.Should().BeEquivalentTo(searchedDevice);
    }

    [Fact]
    public void FindByEmployeId_NonexistingId_ReturnsNull()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entity = repo.FindById(Guid.Parse("abcd5678-abcd-1234-abcd-123456abcdef"));

        entity.Should().Be(null);
    }
}