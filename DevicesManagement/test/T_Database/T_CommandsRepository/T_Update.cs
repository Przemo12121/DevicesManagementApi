using Database.Contexts;
using Database.Models;
using Database.Repositories;
using T_Database;
using Database.Repositories.Builders;
using Database.Models.Interfaces;
using T_Database.T_CommandsRepository.UpdateBuilders;
using FluentAssertions;

namespace T_Database.T_CommandsRepository;

public class T_Update : DeviceMenagementDatabaseTest
{
    public T_Update() : base("Update") { }

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
    public void Update_BuilderModifyingDescription_UpdatesDescription()
    {
        Command entity;
        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new CommandsRepository(context))
            {
                entity = context.Commands.Skip(1).First();

                var builder = new DescriptionUpdateBuilder(entity).SetDescription("New Description");
                repo.Update(builder);
            }
        }

        using (var context = new DeviceManagementContextTest(ContextOptions))
        {
            var entity_after = context.Commands.Where(e => e.Id.Equals(entity.Id)).Single();

            entity_after.Description.Should().Be("New Description");
        }
    }
}