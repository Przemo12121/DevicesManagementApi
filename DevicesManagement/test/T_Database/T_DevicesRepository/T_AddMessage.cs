namespace T_Database.T_DevicesRepository;

public class T_AddMessage : DeviceMenagementDatabaseTest
{
    public T_AddMessage() : base("DevicesRepository.AddMessage") { }

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
        testDevice.Messages.Add(new Message()
        {
            Content = "Message body",
            CreatedDate = DateTime.Now.AddDays(-10),
            Id = Guid.NewGuid(),
            From = "dummy source",
            To = "dummy target"
        });
        testDevice.Messages.Add(new Message()
        {
            Content = "Message body 2",
            CreatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            From = "dummy source",
            To = "dummy target"
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
        otherDevice.Messages.Add(new Message()
        {
            Content = "Message body",
            CreatedDate = DateTime.Now.AddDays(-10),
            Id = Guid.NewGuid(),
            From = "dummy source",
            To = "dummy target"
        });
        context.Devices.Add(otherDevice);

        context.SaveChanges();
    }


    [Fact]
    public void AddMessage_GivenEntity_AddsThatEntity()
    {
        Message entity;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = new Message
                {
                    Content = "New message body",
                    CreatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    From = "dummy source",
                    To = "dummy target"
                };

                repo.AddMessage(testDevice, entity);
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.DevicesMessageHistory.Should().Contain(entity);
        }
    }

    [Fact]
    public void AddMessage_GivenEntity_AddsToSet()
    {
        Message entity;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = new Message
                {
                    Content = "New message body",
                    CreatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    From = "dummy source",
                    To = "dummy target"
                };

                repo.AddMessage(testDevice, entity);
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.DevicesMessageHistory.Should().HaveCount(4);
        }
    }

    [Fact]
    public void AddMessage_GivenMessageAndDevice_CoummandShouldBelongToThatDevice()
    {
        Message entity;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = new Message
                {
                    Content = "New message body",
                    CreatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    From = "dummy source",
                    To = "dummy target"
                };

                repo.AddMessage(testDevice, entity);
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            var testDeviceMessages = context.Devices
                .Where(d => d.Id.Equals(testDevice.Id))
                .SelectMany(d => d.Messages)
                .ToList();

            testDeviceMessages.Should().HaveCount(3);
            testDeviceMessages.Should().Contain(entity);
        }
    }
}