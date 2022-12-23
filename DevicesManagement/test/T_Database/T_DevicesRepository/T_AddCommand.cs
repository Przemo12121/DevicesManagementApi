namespace T_Database.T_DevicesRepository;

public class T_AddCommandHistory : DeviceMenagementDatabaseTest
{
    public T_AddCommandHistory() : base("DevicesRepository.AddCommand") { }

    private Device testDevice = new()
    {
        CreatedDate = DateTime.Now,
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
        testDevice.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now.AddDays(-10),
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "first command"
        });
        testDevice.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now.AddDays(-10),
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "second command"
        });
        context.Devices.Add(testDevice);


        var otherDevice = new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 2",
            Address = "some address 2",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        };
        otherDevice.Commands.Add(new Command()
        {
            Body = "command body",
            CreatedDate = DateTime.Now.AddDays(-10),
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Description = "dummy description",
            Name = "first command"
        });
        context.Devices.Add(otherDevice);

        context.SaveChanges();
    }


    [Fact]
    public void AddCommand_GivenEntity_AddsThatEntity()
    {
        Command entity;

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = new Command
                {
                    CreatedDate = DateTime.Now,
                    Name = "dummy device 3",
                    UpdatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Body = "new command body",
                    Description = "test added command"
                };

                repo.AddCommand(testDevice, entity);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            context.Commands.Should().Contain(entity);
        }
    }

    [Fact]
    public void AddCommand_GivenEntity_AddsToSet()
    {
        Command entity;

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = new Command
                {
                    CreatedDate = DateTime.Now,
                    Name = "dummy device 3",
                    UpdatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Body = "new command body",
                    Description = "test added command"
                };

                repo.AddCommand(testDevice, entity);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            context.Commands.Should().HaveCount(4);
        }
    }

    [Fact]
    public void AddCommand_GivenCommandAndDevice_CoummandShouldBelongToThatDevice()
    {
        Command entity;

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = new Command
                {
                    CreatedDate = DateTime.Now,
                    Name = "dummy device 3",
                    UpdatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Body = "new command body",
                    Description = "test added command"
                };

                repo.AddCommand(testDevice, entity);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            var testDeviceCommands = context.Devices
                .Where(d => d.Id.Equals(testDevice.Id))
                .SelectMany(d => d.Commands)
                .ToList();

            testDeviceCommands.Should().HaveCount(3);
            testDeviceCommands.Should().Contain(entity);
        }
    }
}