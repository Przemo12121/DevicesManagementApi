using T_Database.SearchOptions.MessageOptions;

namespace T_Database.T_DevicesRepository;

public partial class T_GetMessages : DeviceMenagementDatabaseTest
{
    [Fact]
    public void GetMessages_WithDeviceId_ReturnsMessagesBelongingToThatDevice()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetMessages(searched.Id, new LimitableSearchOptions(100));

        entities.Should().HaveCount(2);
        entities.Should().BeEquivalentTo(searched.Messages);
    }

    [Fact]
    public void GetMessages_WithLimitOfOne_ReturnsOneRecord()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int limit = 1;

        var entities = repo.GetMessages(searched.Id, new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void GetMessages_WithOffsetOfOne_ReturnsOneRecord()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int offset = 1;

        var entities = repo.GetMessages(searched.Id, new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void GetMessages_WithOffsetOfOne_ReturnsSecondMessage()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        int offset = 1;

        var entities = repo.GetMessages(searched.Id, new OffsetableSearchOptions(offset));

        entities[0].Should().BeEquivalentTo(searched.Messages[1]);
    }

    [Fact]
    public void GetMessages_WithOrderCreatedDateASC_ReturnsMessagesOrderedByCreatedDateASC()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetMessages(searched.Id, new OrderableByCreatedDateAscSearchOptions());

        entities[0].Should().Be(searched.Messages[0]);
        entities[1].Should().Be(searched.Messages[1]);
    }

    [Fact]
    public void GetMessages_WithOrderCreatedDateDESC_ReturnsMessagesOrderedByCreatedDateDESC()
    {
        using var context = new DeviceManagementContextTest(Key);
        EnsureClear(context);
        Seed(context);
        using var repo = new DevicesRepository(context);

        var entities = repo.GetMessages(searched.Id, new OrderableByCreatedDateDescSearchOptions());

        entities[0].Should().Be(searched.Messages[1]);
        entities[1].Should().Be(searched.Messages[0]);
    }
}

public partial class T_GetMessages
{
    public T_GetMessages() : base("DevicesReopository.GetMessages") { }

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
        searched.Messages.Add(new Message()
        {
            Id = Guid.NewGuid(),
            From = "dummy sourde",
            To = "dummy target",
            Content = "aa first message",
            CreatedDate = DateTime.Now.AddDays(-1),
        });
        searched.Messages.Add(new Message()
        {
            Id = Guid.NewGuid(),
            From = "dummy sourde",
            To = "dummy target",
            Content = "zz second message",
            CreatedDate = DateTime.Now,
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
        otherDevice.Messages.Add(new Message()
        {
            Id = Guid.NewGuid(),
            From = "dummy sourde",
            To = "dummy target",
            Content = "aa first message",
            CreatedDate = DateTime.Now,
        });
        otherDevice.Messages.Add(new Message()
        {
            Id = Guid.NewGuid(),
            From = "dummy sourde",
            To = "dummy target",
            Content = "zz second message",
            CreatedDate = DateTime.Now,
        });
        context.Devices.Add(otherDevice);

        context.SaveChanges();
    }
}