using Database.Contexts;
using Database.Models;
using Database.Repositories;

namespace T_Database.T_CommandsRepository;

public class T_Delete : DeviceMenagementDatabaseTest
{
    private void Seed(DeviceMenagementContext context)
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
    public void Delete_GivenEntity_DeletesThatEntity()
    {
        Command entity;

        using (var context = new DeviceMenagementContext(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity = context.Commands.Skip(1).First();

                repo.Delete(entity);
            }
        }

        using (var context = new DeviceMenagementContext(ContextOptions))
        {
            Assert.DoesNotContain(entity, context.Commands);
        }
    }

    [Fact]
    public void Delete_GivenEntity_DeletesEntityFromSet()
    {
        using (var context = new DeviceMenagementContext(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                var entity = context.Commands.Skip(1).First();

                repo.Delete(entity);
            }
        }

        using (var context = new DeviceMenagementContext(ContextOptions))
        {
            Assert.True(context.Commands.Count() == 2);
        }
    }

    [Fact]
    public void Delete_GivenEntity_DoesNotModifyOthers()
    {
        Command deleted;
        Command entity1;
        Command entity2;

        using (var context = new DeviceMenagementContext(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity1 = context.Commands.Skip(0).First();
                deleted = context.Commands.Skip(1).First();
                entity2 = context.Commands.Skip(2).First();

                repo.Delete(deleted);
            }
        }

        using (var context = new DeviceMenagementContext(ContextOptions))
        {
            var entity1_after = context.Commands.Skip(0).First();
            Assert.Equal(entity1_after.Id, entity1.Id);
            Assert.Equal(entity1_after.Name, entity1.Name);
            Assert.Equal(entity1_after.CreatedDate, entity1.CreatedDate);
            Assert.Equal(entity1_after.UpdatedDate, entity1.UpdatedDate);
            Assert.Equal(entity1_after.Description, entity1.Description);
            Assert.Equal(entity1_after.Body, entity1.Body);

            var entity2_after = context.Commands.Skip(1).First();
            Assert.Equal(entity2_after.Id, entity2.Id);
            Assert.Equal(entity2_after.Name, entity2.Name);
            Assert.Equal(entity2_after.CreatedDate, entity2.CreatedDate);
            Assert.Equal(entity2_after.UpdatedDate, entity2.UpdatedDate);
            Assert.Equal(entity2_after.Description, entity2.Description);
            Assert.Equal(entity2_after.Body, entity2.Body);
        }
    }
}