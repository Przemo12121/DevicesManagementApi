﻿namespace T_Database.T_CommandsRepository;

public partial class T_FindByIdEmployeId
{
    [Fact]
    public void FindByIdAndEmployeId_CorrectIds_ReturnsCammand()
    {
        var entity = Repository.FindByIdAndEmployeeId(Guid.Parse("abcd1234-abcd-1234-abcd-123456abcdef"), "abcd12345678");

        entity.Should().BeEquivalentTo(SearchedCommand);
    }

    [Fact]
    public void FindByIdAndEmployeId_WrongEmployeeId_ReturnsNull()
    {
        var entity = Repository.FindByIdAndEmployeeId(Guid.Parse("abcd1234-abcd-1234-abcd-123456abcdef"), "badx12345678");

        entity.Should().BeNull();
    }

    [Fact]
    public void FindByIdAndEmployeId_WrongId_ReturnsNull()
    {
        var entity = Repository.FindByIdAndEmployeeId(Guid.Parse("12345678-1234-1234-1234-123456123456"), "abcd12345678");

        entity.Should().BeNull();
    }
}

public partial class T_FindByIdEmployeId : IClassFixture<T_FindByIdEmployeId_Setup>
{
    private readonly T_FindByIdEmployeId_Setup _setupFixture;
    CommandsRepository Repository { get; init; }
    public Command SearchedCommand { get; init; }


    public T_FindByIdEmployeId(T_FindByIdEmployeId_Setup setupFixture)
    {
        _setupFixture = setupFixture;
        SearchedCommand = setupFixture.SearchedCommand;
        Repository = new CommandsRepository(setupFixture.Context);
    }
}

public class T_FindByIdEmployeId_Setup : DeviceMenagementDatabaseTest
{
    public DeviceManagementContextTest Context { get; init; }
    public Command SearchedCommand { get; } = new Command()
    {
        CreatedDate = DateTime.Now,
        Name = "dummy command",
        UpdatedDate = DateTime.Now,
        Id = Guid.Parse("abcd1234-abcd-1234-abcd-123456abcdef"),
        Body = "dummy body",
        CommandHistories = new List<CommandHistory>(),
        Description = "dummy description"
    };

    public T_FindByIdEmployeId_Setup() : base("CommandsRepository.FindByIdEmployeeId")
    {
        Context = new DeviceManagementContextTest("CommandsRepository.FindByIdEmployeeId");
        Seed(Context);
    }

    private void Seed(DeviceManagementContextTest context)
    { 
        var dummyCommand2 = new Command()
        {
            CreatedDate = DateTime.Now,
            Name = "dummy command 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Body = "dummy body 2",
            CommandHistories = new List<CommandHistory>(),
            Description = "dummy description 2"
        };
        var dummyDevice = new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "abcd12345678",
            Address = "dummy address",
            Commands = new List<Command>(new[] { SearchedCommand, dummyCommand2 }),
            Messages = new List<Message>()
        };
        context.Commands.AddRange(dummyDevice.Commands);
        context.Devices.Add(dummyDevice);

        var dummyCommand3 = new Command()
        {
            CreatedDate = DateTime.Now,
            Name = "dummy command 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Body = "dummy body 3",
            CommandHistories = new List<CommandHistory>(),
            Description = "dummy description 3"
        };
        var dummyCommand4 = new Command()
        {
            CreatedDate = DateTime.Now,
            Name = "dummy command 4",
            UpdatedDate = DateTime.Now,
            Id = Guid.Parse("12345678-1234-1234-1234-123456123456"),
            Body = "dummy body 4",
            CommandHistories = new List<CommandHistory>(),
            Description = "dummy description 4"
        };
        var dummyDevice2 = new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "badx12345678",
            Address = "dummy address",
            Commands = new List<Command>(new[] {  dummyCommand3, dummyCommand4 }),
            Messages = new List<Message>()
        };
        context.Commands.AddRange(dummyDevice2.Commands);
        context.Devices.Add(dummyDevice2);
        context.SaveChanges();
    }
}