namespace T_Database.T_DevicesRepository;

[Collection("RepositoriesTests")]
public partial class T_FindById
{
    [Fact]
    public async void FindByEmployeId_ExistingId_ReturnsDevicesWithThatId()
    {
        var entity = await Repository.FindByIdAsync(Guid.Parse("12345678-abcd-1234-abcd-123456abcdef"));

        entity.Should().BeEquivalentTo(SearchedDevice);
    }

    [Fact]
    public async void FindByEmployeId_NonexistingId_ReturnsNull()
    {
        var entity = await Repository.FindByIdAsync(Guid.Parse("abcd5678-abcd-1234-abcd-123456abcdef"));

        entity.Should().Be(null);
    }
}

public partial class T_FindById : IClassFixture<T_FindById_Setup>
{
    private readonly T_FindById_Setup _setupFixture;
    Device SearchedDevice { get; init; }
    private DevicesRepository Repository { get; init; }

    public T_FindById(T_FindById_Setup setupFixture)
    {
        _setupFixture = setupFixture;
        SearchedDevice = setupFixture.SearchedDevice;
        Repository = new DevicesRepository(setupFixture.Context);
    }
}

public class T_FindById_Setup : DeviceMenagementDatabaseTest
{
    public DeviceManagementContextTest Context { get; init; }

    public Device SearchedDevice { get; } = new()
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

    public T_FindById_Setup() : base("DevicesRepository.FindById") 
    {
        Context = new DeviceManagementContextTest("DevicesRepository.FindById");
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
}