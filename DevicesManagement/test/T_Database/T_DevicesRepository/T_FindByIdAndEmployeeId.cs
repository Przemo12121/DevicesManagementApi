namespace T_Database.T_DevicesRepository;

public partial class T_FindByIdEmployeId
{
    [Fact]
    public void FindByIdAndEmployeId_CorrectIds_ReturnsDevice()
    {
        var entity = Repository.FindByIdAndOwnerId(Guid.Parse("abcd1234-abcd-1234-abcd-123456abcdef"), "abcd12345678");

        entity.Should().BeEquivalentTo(SearchedDevice);
    }

    [Fact]
    public void FindByIdAndEmployeId_WrongEmployeeId_ReturnsNull()
    {
        var entity = Repository.FindByIdAndOwnerId(Guid.Parse("abcd1234-abcd-1234-abcd-123456abcdef"), "badx12345678");

        entity.Should().BeNull();
    }

    [Fact]
    public void FindByIdAndEmployeId_WrongId_ReturnsNull()
    {
        var entity = Repository.FindByIdAndOwnerId(Guid.Parse("12345678-1234-1234-1234-123456123456"), "abcd12345678");

        entity.Should().BeNull();
    }
}

public partial class T_FindByIdEmployeId : IClassFixture<T_FindByIdEmployeId_Setup>
{
    private readonly T_FindByIdEmployeId_Setup _setupFixture;
    DevicesRepository Repository { get; init; }
    public Device SearchedDevice { get; init; }


    public T_FindByIdEmployeId(T_FindByIdEmployeId_Setup setupFixture)
    {
        _setupFixture = setupFixture;
        SearchedDevice = setupFixture.SearchedDevice;
        Repository = new DevicesRepository(setupFixture.Context);
    }
}

public class T_FindByIdEmployeId_Setup : DeviceMenagementDatabaseTest
{
    public DeviceManagementContextTest Context { get; init; }
    public Device SearchedDevice { get; } = new ()
    {
        CreatedDate = DateTime.Now,
        Name = "dummy device",
        UpdatedDate = DateTime.Now,
        Id = Guid.Parse("abcd1234-abcd-1234-abcd-123456abcdef"),
        EmployeeId = "abcd12345678",
        Address = "dummy address",
        Commands = new List<Command>(),
        Messages = new List<Message>()
    };

    public T_FindByIdEmployeId_Setup() : base("Devicesepostory.FindByIdEmployeeId")
    {
        Context = new DeviceManagementContextTest("Devicesepostory.FindByIdEmployeeId");
        Seed(Context);
    }

    private void Seed(DeviceManagementContextTest context)
    { 
        context.Devices.Add(SearchedDevice);
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "badx12345678",
            Address = "dummy address",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.SaveChanges();
    }
}