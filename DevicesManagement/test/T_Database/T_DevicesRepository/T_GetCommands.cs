using T_Database.SearchOptions.CommandOptions;

namespace T_Database.T_DevicesRepository;

public partial class T_GetCommands : DeviceMenagementDatabaseTest
{
    [Fact]
    public void GetCommands_WithDeviceId_ReturnsCommandsBelongingToThatDevice()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetCommands(searched.Id, new LimitableSearchOptions(100));

        entities.Should().HaveCount(2);
        entities.Should().BeEquivalentTo(searched.Commands);
    }

    [Fact]
    public void GetCommands_WithLimitOfOne_ReturnsOneRecord()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int limit = 1;

        var entities = repo.GetCommands(searched.Id, new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void GetCommands_WithOffsetOfOne_ReturnsOneRecord()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int offset = 1;

        var entities = repo.GetCommands(searched.Id, new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void GetCommands_WithOffsetOfOne_ReturnsSecondCommand()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int offset = 1;

        var entities = repo.GetCommands(searched.Id, new OffsetableSearchOptions(offset));

        entities[0].Should().BeEquivalentTo(searched.Commands[1]);
    }

    [Fact]
    public void GetCommands_WithOrderNameASC_ReturnsCommandsOrderByNameASC()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetCommands(searched.Id, new OrderableByNameAscSearchOptions());

        entities[0].Name.Should().Be("a first command");
        entities[1].Name.Should().Be("z second command");
    }

    [Fact]
    public void GetCommands_WithOrderNameDESC_ReturnsCommandsOrderByNameDESC()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetCommands(searched.Id, new OrderableByNameDescSearchOptions());

        entities[0].Name.Should().Be("z second command");
        entities[1].Name.Should().Be("a first command");
    }
}

public partial class T_GetCommands
{
    public T_GetCommands() : base("DevicesReopository.GetCommands") { }

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
            Name = "a first command"
        });
        searched.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "z second command"
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