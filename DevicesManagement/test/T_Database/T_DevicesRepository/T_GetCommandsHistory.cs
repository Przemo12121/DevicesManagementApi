using T_Database.SearchOptions.CommandHistoryOptions;

namespace T_Database.T_DevicesRepository;

public class T_GetCommandsHistory : DeviceMenagementDatabaseTest
{
    public T_GetCommandsHistory() : base("DevicesReopository.GetCommandsHistory") { }

    Device searched = new()
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

    private void Seed(DeviceManagementContextTest context)
    {
        searched.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now.AddDays(-10),
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "a first command",
            CommandHistories = new List<CommandHistory>()
        });
        searched.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "z second command",
            CommandHistories = new List<CommandHistory>()
        });
        searched.Commands[0].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-3)
        });
        searched.Commands[1].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-2)
        });
        searched.Commands[0].CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-1)
        });
        context.Devices.Add(searched);

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


    [Fact]
    public void GetCommandsHistory_WithDeviceId_ReturnsCommandsHistoryBelongingToThatDevice()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetCommandHistories(searched.Id, new LimitableSearchOptions(100));

        entities.Should().HaveCount(3);
        entities.Should().BeEquivalentTo(searched.Commands.SelectMany(c => c.CommandHistories));
    }

    [Fact]
    public void GetCommandsHistory_WithLimitOfTwo_ReturnsTwoRecords()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int limit = 2;

        var entities = repo.GetCommandHistories(searched.Id, new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void GetCommandsHistory_WithOffsetOfTwo_ReturnsOneRecord()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int offset = 2;

        var entities = repo.GetCommandHistories(searched.Id, new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void GetCommandsHistory_WithOffsetOfTwo_ReturnsThirdCommandHistory()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int offset = 2;

        var entities = repo.GetCommandHistories(searched.Id, new OffsetableSearchOptions(offset));

        entities[0].Should().BeEquivalentTo(searched.Commands[0].CommandHistories[1]);
    }

    [Fact]
    public void GetCommandsHistory_WithOrderCreatedDateASC_ReturnsCommandsOrderByCreatedDateASC()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetCommandHistories(searched.Id, new OrderableByCreatedDateAscSearchOptions());

        entities[0].Should().BeEquivalentTo(searched.Commands[0].CommandHistories[0]);
        entities[1].Should().BeEquivalentTo(searched.Commands[1].CommandHistories[0]);
        entities[2].Should().BeEquivalentTo(searched.Commands[0].CommandHistories[1]);
    }

    [Fact]
    public void GetCommandsHistory_WithOrderCreatedDateDESC_ReturnsCommandsOrderByCreatedDateDESC()
    {
        using var context = new DeviceManagementContextTest(ContextOptions);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetCommandHistories(searched.Id, new OrderableByCreatedDateDescSearchOptions());

        entities[0].Should().BeEquivalentTo(searched.Commands[0].CommandHistories[1]);
        entities[1].Should().BeEquivalentTo(searched.Commands[1].CommandHistories[0]);
        entities[2].Should().BeEquivalentTo(searched.Commands[0].CommandHistories[0]);
    }
}