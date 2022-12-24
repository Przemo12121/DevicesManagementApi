using T_Database.SearchOptions.DeviceOptions;

namespace T_Database.T_DevicesRepository;

public class T_Update : DeviceMenagementDatabaseTest
{
    public T_Update() : base("DevocesRepository.Update") { }

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
        context.Devices.Add(new Device
        {
            CreatedDate = DateTime.Now,
            Name = "dummy device 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some employee id 3",
            Address = "some address 3",
            Commands = new List<Command>(),
            Messages = new List<Message>()
        });
        context.SaveChanges();
    }

    
    [Fact]
    public void Update_Name_UpdatesName()
    {
        Device entity;
        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = context.Devices.Skip(1).First();

                entity.Name = "New Name";
                repo.Update(entity);
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            var entity_after = context.Devices.Where(e => e.Id.Equals(entity.Id)).Single();

            entity_after.Name.Should().Be("New Name");
        }
    }

    [Fact]
    public void Update_Name_DoesNotUpdatesOtherAttributes()
    {
        Device entity;
        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = context.Devices.Skip(1).First();

                entity.Name = "New Name";
                repo.Update(entity);
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            var entity_after = context.Devices.Where(e => e.Id.Equals(entity.Id)).Single();

            entity_after.CreatedDate.Should().Be(entity.CreatedDate);
            entity_after.UpdatedDate.Should().Be(entity.UpdatedDate);
            entity_after.Address.Should().Be(entity.Address);
        }
    }

    [Fact]
    public void Update_Name_DoesNotUpdatesOthers()
    {
        Device entityUpdated;
        Device entity1;
        Device entity2;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity1 = context.Devices.Skip(0).First();
                entityUpdated = context.Devices.Skip(1).First();
                entity2 = context.Devices.Skip(2).First();

                entityUpdated.Name = "New Name";
                repo.Update(entityUpdated);
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            var entity1_after = context.Devices.Where(e => e.Id.Equals(entity1.Id)).Single();
            entity1_after.Should().BeEquivalentTo(entity1);

            var entity2_after = context.Devices.Where(e => e.Id.Equals(entity2.Id)).Single();
            entity2_after.Should().BeEquivalentTo(entity2);
        }
    }
}