namespace T_Database.T_DevicesRepository;

public partial class T_Delete : DeviceMenagementDatabaseTest
{
    [Fact]
    public void Delete_GivenEntity_DeletesThatEntity()
    {
        Device entity;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity = context.Devices.Skip(1).First();

                repo.Delete(entity);
                repo.SaveAsync();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.Devices.Should().NotContain(entity);
        }
    }

    [Fact]
    public void Delete_GivenEntity_DeletesEntityFromSet()
    {
        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                var entity = context.Devices.Skip(1).First();

                repo.Delete(entity);
                repo.SaveAsync();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.Devices.Should().HaveCount(2);
        }
    }

    [Fact]
    public void Delete_GivenEntity_DoesNotModifyOthers()
    {
        Device deleted;
        Device entity1;
        Device entity2;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new DevicesRepository(context))
            {
                entity1 = context.Devices.Skip(0).First();
                deleted = context.Devices.Skip(1).First();
                entity2 = context.Devices.Skip(2).First();

                repo.Delete(deleted);
                repo.SaveAsync();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            var entity1_after = context.Devices.Skip(0).First();
            entity1_after.Should().BeEquivalentTo(entity1);
            
            var entity2_after = context.Devices.Skip(1).First();
            entity2_after.Should().BeEquivalentTo(entity2);
        }
    }
}

public partial class T_Delete
{
    public T_Delete() : base("DevicesRepository.Delete") { }

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
}