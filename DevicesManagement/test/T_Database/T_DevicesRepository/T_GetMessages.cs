using T_Database.SearchOptions.MessageOptions;

namespace T_Database.T_DevicesRepository;

public partial class T_GetMessages
{
    [Fact]
    public void GetMessages_WithDeviceId_ReturnsMessagesBelongingToThatDevice()
    {
        var entities = Repository.GetMessages(Searched.Id, new LimitableSearchOptions(100));

        entities.Should().HaveCount(2);
        entities.Should().BeEquivalentTo(Searched.Messages);
    }

    [Fact]
    public void GetMessages_WithLimitOfOne_ReturnsOneRecord()
    {
        int limit = 1;
        var entities = Repository.GetMessages(Searched.Id, new LimitableSearchOptions(limit));

        entities.Should().HaveCount(limit);
    }

    [Fact]
    public void GetMessages_WithOffsetOfOne_ReturnsOneRecord()
    {
        int offset = 1;
        var entities = Repository.GetMessages(Searched.Id, new OffsetableSearchOptions(offset));

        entities.Should().HaveCount(1);
    }

    [Fact]
    public void GetMessages_WithOffsetOfOne_ReturnsSecondMessage()
    {
        int offset = 1;
        var entities = Repository.GetMessages(Searched.Id, new OffsetableSearchOptions(offset));

        entities[0].Should().BeEquivalentTo(Searched.Messages[1]);
    }

    [Fact]
    public void GetMessages_WithOrderCreatedDateASC_ReturnsMessagesOrderedByCreatedDateASC()
    {
        var entities = Repository.GetMessages(Searched.Id, new OrderableByCreatedDateAscSearchOptions());

        entities[0].Should().Be(Searched.Messages[0]);
        entities[1].Should().Be(Searched.Messages[1]);
    }

    [Fact]
    public void GetMessages_WithOrderCreatedDateDESC_ReturnsMessagesOrderedByCreatedDateDESC()
    {
        var entities = Repository.GetMessages(Searched.Id, new OrderableByCreatedDateDescSearchOptions());

        entities[0].Should().Be(Searched.Messages[1]);
        entities[1].Should().Be(Searched.Messages[0]);
    }
}

public partial class T_GetMessages : IClassFixture<T_GetMessages_Setup>
{
    private readonly T_GetMessages_Setup _setupFixture;
    Device Searched { get; init; }
    DevicesRepository Repository { get ; init; }

    public T_GetMessages(T_GetMessages_Setup setupFixture) 
    {
        _setupFixture = setupFixture;
        Repository = new DevicesRepository(setupFixture.Context);
        Searched = setupFixture.Searched;
    }
}

public class T_GetMessages_Setup : DeviceMenagementDatabaseTest
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

    public T_GetMessages_Setup() : base("DevicesReopository.GetMessages") 
    {
        Context = new DeviceManagementContextTest("DevicesReopository.GetMessages");
        Seed(Context);
    }

    private void Seed(DeviceManagementContextTest context)
    {
        Searched.Messages.Add(new Message()
        {
            Id = Guid.NewGuid(),
            From = "dummy sourde",
            To = "dummy target",
            Content = "aa first message",
            CreatedDate = DateTime.Now.AddDays(-1),
        });
        Searched.Messages.Add(new Message()
        {
            Id = Guid.NewGuid(),
            From = "dummy sourde",
            To = "dummy target",
            Content = "zz second message",
            CreatedDate = DateTime.Now,
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