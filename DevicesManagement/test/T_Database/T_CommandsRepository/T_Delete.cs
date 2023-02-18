namespace T_Database.T_CommandsRepository;

public partial class T_Delete : DeviceMenagementDatabaseTest
{
   
    [Fact]
    public void Delete_GivenEntity_DeletesThatEntity()
    {
        Command entity;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity = context.Commands.Skip(1).First();

                repo.Delete(entity);
                repo.SaveAsync();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.Commands.Should().NotContain(entity);
        }
    }

    [Fact]
    public void Delete_GivenEntity_DeletesEntityFromSet()
    {
        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                var entity = context.Commands.Skip(1).First();

                repo.Delete(entity);
                repo.SaveAsync();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            context.Commands.Should().HaveCount(2);
        }
    }

    [Fact]
    public void Delete_GivenEntity_DoesNotModifyOthers()
    {
        Command deleted;
        Command entity1;
        Command entity2;

        using (var context = new DeviceManagementContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity1 = context.Commands.Skip(0).First();
                deleted = context.Commands.Skip(1).First();
                entity2 = context.Commands.Skip(2).First();

                repo.Delete(deleted);
                repo.SaveAsync();
            }
        }

        using (var context = new DeviceManagementContextTest(Key))
        {
            var entity1_after = context.Commands.Skip(0).First();
            entity1_after.Should().BeEquivalentTo(entity1);
            
            var entity2_after = context.Commands.Skip(1).First();
            entity2_after.Should().BeEquivalentTo(entity2);
        }
    }
}

public partial class T_Delete
{
    public T_Delete() : base("CommandsRepository.Delete") { }

    private void Seed(DeviceManagementContextTest context)
    {
        context.Commands.Add(new Command
        {
            Body = "ping .",
            CreatedDate = DateTime.Now,
            Name = "ping",
            Description = "this command pings device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid()
        });
        context.Commands.Add(new Command
        {
            Body = "on",
            CreatedDate = DateTime.Now,
            Name = "turn on",
            Description = "this command turns on device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid()
        });
        context.Commands.Add(new Command
        {
            Body = "off",
            CreatedDate = DateTime.Now,
            Name = "turn off",
            Description = "this command turns off device",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid()
        });
        context.SaveChanges();
    }

}