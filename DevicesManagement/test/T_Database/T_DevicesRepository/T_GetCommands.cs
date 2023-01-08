using Database.Repositories;
using T_Database.SearchOptions.CommandOptions;

namespace T_Database.T_DevicesRepository;

public partial class T_GetCommands
{
    [Fact]
    public void GetCommands_WithDeviceId_ReturnsCommandsBelongingToThatDevice()
    {
        var entities = Repository.GetCommands(Searched.Id, new LimitableSearchOptions(100));

        entities.Should().HaveCount(2);
        entities.Should().BeEquivalentTo(Searched.Commands);
    }

    [Fact]
    public void GetCommands_WithLimitOfOne_ReturnsOneRecord()
    {
        int limit = 1;
        var entities = Repository.GetCommands(Searched.Id, new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void GetCommands_WithOffsetOfOne_ReturnsOneRecord()
    {
        int offset = 1;
        var entities = Repository.GetCommands(Searched.Id, new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void GetCommands_WithOffsetOfOne_ReturnsSecondCommand()
    {
        int offset = 1;
        var entities = Repository.GetCommands(Searched.Id, new OffsetableSearchOptions(offset));

        entities[0].Should().BeEquivalentTo(Searched.Commands[1]);
    }

    [Fact]
    public void GetCommands_WithOrderNameASC_ReturnsCommandsOrderByNameASC()
    {
        var entities = Repository.GetCommands(Searched.Id, new OrderableByNameAscSearchOptions());

        entities[0].Name.Should().Be("a first command");
        entities[1].Name.Should().Be("z second command");
    }

    [Fact]
    public void GetCommands_WithOrderNameDESC_ReturnsCommandsOrderByNameDESC()
    {
        var entities = Repository.GetCommands(Searched.Id, new OrderableByNameDescSearchOptions());

        entities[0].Name.Should().Be("z second command");
        entities[1].Name.Should().Be("a first command");
    }
}

public partial class T_GetCommands : IClassFixture<T_GetCommands_Setup>
{
    private readonly T_GetCommands_Setup _setupFixture;
    Device Searched { get; init; } 
    DevicesRepository Repository { get; init; }

    public T_GetCommands(T_GetCommands_Setup setupFixture)
    {
        _setupFixture = setupFixture;
        Searched = setupFixture.Searched;
        Repository = new DevicesRepository(setupFixture.Context);
    }
}

public class T_GetCommands_Setup : DeviceMenagementDatabaseTest
{
    public DeviceManagementContextTest Context { get; init; }
    public Device Searched { get; } = new ()
    {
        CreatedDate = DateTime.Now.AddDays(-10),
        Name = "dummy device",
        UpdatedDate = DateTime.Now,
        Id = Guid.NewGuid(),
        EmployeeId = "some employee id",
        Address = "some address",
        Commands = new List<Command>(),
        Messages = new List<Message>()
    };

    public T_GetCommands_Setup() : base("DevicesReopository.GetCommands") 
    {
        Context = new DeviceManagementContextTest("DevicesReopository.GetCommands");
        Seed(Context);
    }

    private void Seed(DeviceManagementContextTest context)
    {
        Searched.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now.AddDays(-10),
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "a first command"
        });
        Searched.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "z second command"
        });

        context.Devices.Add(Searched);
            
        var otherDevice = new Device()
        {
            CreatedDate = DateTime.Now.AddDays(-10),
            Name = "dummy device 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id",
            Address = "some address 2",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        };
        otherDevice.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "other command"
        });
        otherDevice.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "other command 2"
        });

        context.SaveChanges();
    }
}