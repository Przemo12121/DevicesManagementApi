using T_Database.SearchOptions.DeviceOptions;

namespace T_Database.T_DevicesRepository;

public partial class T_Add : DeviceMenagementDatabaseTest
{
   
    [Fact]
    public void Add_GivenEntity_AddsThatEntity()
    {
        Device entity;

        using (var context = new DeviceManagementContextTest(Key))
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
                    Commands = new List<Command>(),
                    Messages = new List<Message>()
                };

                repo.Add(entity);
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.Devices.Should().Contain(entity);
        }
    }

    [Fact]
    public void Add_GivenEntity_AddsToSet()
    {
        Device entity;

        using (var context = new DeviceManagementContextTest(Key))
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
                    Commands = new List<Command>(),
                    Messages = new List<Message>()
                };

                repo.Add(entity);
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.Devices.Should().HaveCount(3);
        }
    }
}

public partial class T_Add
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
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 2",
            Address = "some address 2",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.SaveChanges();
    }
}