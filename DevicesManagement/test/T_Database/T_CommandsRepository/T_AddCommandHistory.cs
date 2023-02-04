namespace T_Database.T_CommandsRepository;

public partial class T_AddCommandHistory : DeviceMenagementDatabaseTest
{
    [Fact]
    public void AddCommand_GivenEntity_AddsThatEntity()
    {
        CommandHistory entity;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity = new CommandHistory
                {
                    CreatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                };

                repo.AddCommandHistory(testCommand, entity);
                repo.SaveChanges();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.DevicesCommandHistory.Should().Contain(entity);
        }
    }

    [Fact]
    public void AddCommand_GivenEntity_AddsToSet()
    {
        CommandHistory entity;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity = new CommandHistory
                {
                    CreatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                };

                repo.AddCommandHistory(testCommand, entity);
                repo.SaveChanges();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.DevicesCommandHistory.Should().HaveCount(4);
        }
    }

    [Fact]
    public void AddCommand_GivenCommandHistoryAndCommand_CoummandHistoryShouldBelongToThatCommand()
    {
        CommandHistory entity;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity = new CommandHistory
                {
                    CreatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                };

                repo.AddCommandHistory(testCommand, entity);
                repo.SaveChanges();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            var testCommandsCommandHistories = context.Commands
                .Where(c => c.Id.Equals(testCommand.Id))
                .SelectMany(c => c.CommandHistories)
                .ToList();

            testCommandsCommandHistories.Should().HaveCount(3);
            testCommandsCommandHistories.Should().Contain(entity);
        }
    }
}

public partial class T_AddCommandHistory
{
    public T_AddCommandHistory() : base("CommandsRepository.AddCommandHistory") { }

    private Command testCommand = new()
    {
        CreatedDate = DateTime.Now,
        Name = "dummy command",
        UpdatedDate = DateTime.Now,
        Id = Guid.NewGuid(),
        CommandHistories = new List<CommandHistory>(),
        Body = "dummy command body",
        Description = "dummy command description"
    };

    private void Seed(DeviceManagementContextTest context)
    {
        testCommand.CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-10),
            Id = Guid.NewGuid()
        });
        testCommand.CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-10),
            Id = Guid.NewGuid(),
        });
        context.Commands.Add(testCommand);


        var otherCommand = new Command
        {
            CreatedDate = DateTime.Now,
            Name = "dummy command 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            CommandHistories = new List<CommandHistory>(),
            Body = "dummy command body 2",
            Description = "dummy command description 2"
        };
        otherCommand.CommandHistories.Add(new CommandHistory()
        {
            CreatedDate = DateTime.Now.AddDays(-10),
            Id = Guid.NewGuid(),
        });
        context.Commands.Add(otherCommand);

        context.SaveChanges();
    }
}