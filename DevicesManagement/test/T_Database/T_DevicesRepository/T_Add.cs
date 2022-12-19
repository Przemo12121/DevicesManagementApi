namespace T_Database.T_DevicesRepository;

public class T_Add : DeviceMenagementDatabaseTest
{
    public T_Add() : base("DevicesRepository.Add") { }

    private void Seed(DeviceManagementContextTest context)
    {
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id",
            Address = "some address",
            CommandHistory = new List<CommandHistory>(),
            Commands = new List<Command>(),
            MessageHistory = new List<Message>()
        }) ;
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 2",
            Address = "some address 2",
            CommandHistory = new List<CommandHistory>(),
            Commands = new List<Command>(),
            MessageHistory = new List<Message>()
        });
        context.SaveChanges();
    }


    [Fact]
    public void Add_GivenEntity_AddsThatEntity()
    {
        Device entity;

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = new Device
                {
                    CreatedDate = DateTime.Now,
                    Name = "dummy device 3",
                    UpdatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    EmployeeId = "some employee id 3",
                    Address = "some address 3",
                    CommandHistory = new List<CommandHistory>(),
                    Commands = new List<Command>(),
                    MessageHistory = new List<Message>()
                };

                repo.Add(entity);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            context.Devices.Should().Contain(entity);
        }
    }

    [Fact]
    public void Add_GivenEntity_AddsToSet()
    {
        Device entity;

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = new Device
                {
                    CreatedDate = DateTime.Now,
                    Name = "dummy device 3",
                    UpdatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    EmployeeId = "some employee id 3",
                    Address = "some address 3",
                    CommandHistory = new List<CommandHistory>(),
                    Commands = new List<Command>(),
                    MessageHistory = new List<Message>()
                };

                repo.Add(entity);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            context.Devices.Should().HaveCount(3);
        }
    }
}