using T_Database.SearchOptions.CommandHistoryOptions;

namespace T_Database.T_DevicesRepository;

public partial class T_GetCommandsHistory
{
    [Fact]
    public async void GetCommandsHistory_WithDeviceId_ReturnsCommandsHistoryBelongingToThatDevice()
    {
        var entities = await Repository.GetCommandHistoriesAsync(Searched.Id, new LimitableSearchOptions(100));

        entities.Should().HaveCount(3);
        entities.Should().BeEquivalentTo(Searched.Commands.SelectMany(c => c.CommandHistories));
    }

    [Fact]
    public async void GetCommandsHistory_WithLimitOfTwo_ReturnsTwoRecords()
    {
        int limit = 2;
        var entities = await Repository.GetCommandHistoriesAsync(Searched.Id, new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public async void GetCommandsHistory_WithOffsetOfTwo_ReturnsOneRecord()
    {
        int offset = 2;
        var entities = await Repository.GetCommandHistoriesAsync(Searched.Id, new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public async void GetCommandsHistory_WithOffsetOfTwo_ReturnsThirdCommandHistory()
    {
        int offset = 2;
        var entities = await Repository.GetCommandHistoriesAsync(Searched.Id, new OffsetableSearchOptions(offset));

        entities[0].Should().BeEquivalentTo(Searched.Commands[0].CommandHistories[1]);
    }

    [Fact]
    public async void GetCommandsHistory_WithOrderCreatedDateASC_ReturnsCommandsOrderByCreatedDateASC()
    {
        var entities = await Repository.GetCommandHistoriesAsync(Searched.Id, new OrderableByCreatedDateAscSearchOptions());

        entities[0].Should().BeEquivalentTo(Searched.Commands[0].CommandHistories[0]);
        entities[1].Should().BeEquivalentTo(Searched.Commands[1].CommandHistories[0]);
        entities[2].Should().BeEquivalentTo(Searched.Commands[0].CommandHistories[1]);
    }

    [Fact]
    public async void GetCommandsHistory_WithOrderCreatedDateDESC_ReturnsCommandsOrderByCreatedDateDESC()
    {
        var entities = await Repository.GetCommandHistoriesAsync(Searched.Id, new OrderableByCreatedDateDescSearchOptions());

        entities[0].Should().BeEquivalentTo(Searched.Commands[0].CommandHistories[1]);
        entities[1].Should().BeEquivalentTo(Searched.Commands[1].CommandHistories[0]);
        entities[2].Should().BeEquivalentTo(Searched.Commands[0].CommandHistories[0]);
    }
}

public partial class T_GetCommandsHistory : IClassFixture<T_GetCommandsHistory_Setup>
{
    private readonly T_GetCommandsHistory_Setup _setupFixture;
    Device Searched { get; init; }
    DevicesRepository Repository { get; init; }
    public T_GetCommandsHistory(T_GetCommandsHistory_Setup setupFixture) 
    {
        _setupFixture = setupFixture;
        Repository = new DevicesRepository(setupFixture.Context);
        Searched = setupFixture.Searched;
    }

}

public class T_GetCommandsHistory_Setup : DeviceMenagementDatabaseTest
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

    public T_GetCommandsHistory_Setup() : base("DevicesReopository.GetCommandsHistory") 
    {
        Context = new DeviceManagementContextTest("DevicesReopository.GetCommandsHistory");
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
            Name = "a first command",
            CommandHistories = new List<CommandHistory>()
        });
        Searched.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "z second command",
            CommandHistories = new List<CommandHistory>()
        });
        Searched.Commands[0].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-3)
        });
        Searched.Commands[1].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-2)
        });
        Searched.Commands[0].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-1)
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
            Name = "other command",
            CommandHistories = new List<CommandHistory>()
        });
        otherDevice.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "other command 2",
            CommandHistories = new List<CommandHistory>()
        });
        otherDevice.Commands[0].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-3)
        });
        otherDevice.Commands[1].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-2)
        });
        otherDevice.Commands[0].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-1)
        });

        context.SaveChanges();
    }
}