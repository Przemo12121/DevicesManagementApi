namespace T_Database.T_CommandsRepository;

public class T_Update : DeviceMenagementDatabaseTest
{
    public T_Update() : base("CommandsRepository.Update") { }

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

    
    [Fact]
    public void Update_Description_UpdatesDescription()
    {
        Command entity;
        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity = context.Commands.Skip(1).First();

                entity.Description = "New Description";
                repo.Update(entity);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            var entity_after = context.Commands.Where(e => e.Id.Equals(entity.Id)).Single();

            entity_after.Description.Should().Be("New Description");
        }
    }

    [Fact]
    public void Update_Description_DoesNotUpdatesOtherAttributes()
    {
        Command entity;
        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity = context.Commands.Skip(1).First();

                entity.Description = "New Description";
                repo.Update(entity);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            var entity_after = context.Commands.Where(e => e.Id.Equals(entity.Id)).Single();

            entity_after.Name.Should().Be(entity.Name);
            entity_after.CreatedDate.Should().Be(entity.CreatedDate);
            entity_after.UpdatedDate.Should().Be(entity.UpdatedDate);
            entity_after.Body.Should().Be(entity.Body);
        }
    }

    [Fact]
    public void Update_Description_DoesNotUpdatesOthers()
    {
        Command entityUpdated;
        Command entity1;
        Command entity2;

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity1 = context.Commands.Skip(0).First();
                entityUpdated = context.Commands.Skip(1).First();
                entity2 = context.Commands.Skip(2).First();

                entityUpdated.Description = "New Description";
                repo.Update(entityUpdated);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            var entity1_after = context.Commands.Where(e => e.Id.Equals(entity1.Id)).Single();
            entity1_after.Should().BeEquivalentTo(entity1);

            var entity2_after = context.Commands.Where(e => e.Id.Equals(entity2.Id)).Single();
            entity2_after.Should().BeEquivalentTo(entity2);
        }
    }
}