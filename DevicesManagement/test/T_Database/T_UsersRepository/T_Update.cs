using T_Database.SearchOptions.UserOptions;

namespace T_Database.T_UsersRepository;

public class T_Update : LocalAuthDatabaseTest
{
    public T_Update() : base("Update") { }

    private void Seed(LocalAuthContextTest context)
    {
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id",
            Password = "password",
            Enabled = true,
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Admin }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 2",
            Password = "password",
            Enabled = false,
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Employee }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 3",
            Password = "password",
            Enabled = false,
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Employee }
        });
        context.SaveChanges();
    }


    [Fact]
    public void Update_Name_UpdatesName()
    {
        User entity;
        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new UsersRepository(context))
            {
                entity = context.Users.Skip(1).First();

                entity.Name = "New Name";
                repo.Update(entity);
            }
        }

        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            var entity_after = context.Users.Where(e => e.Id.Equals(entity.Id)).Single();

            entity_after.Name.Should().Be("New Name");
        }
    }

    [Fact]
    public void Update_Name_DoesNotUpdatesOtherAttributes()
    {
        User entity;
        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new UsersRepository(context))
            {
                entity = context.Users.Skip(1).First();

                entity.Name = "New Name";
                repo.Update(entity);
            }
        }

        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            var entity_after = context.Users.Where(e => e.Id.Equals(entity.Id)).Single();

            entity_after.CreatedDate.Should().Be(entity.CreatedDate);
            entity_after.UpdatedDate.Should().Be(entity.UpdatedDate);
            entity_after.EmployeeId.Should().Be(entity.EmployeeId);
            entity_after.Enabled.Should().Be(entity.Enabled);
            //entity_after.AccessLevel.Value.Should().Be(entity.AccessLevel.Value);
            //entity_after.AccessLevel.Id.Should().Be(entity.AccessLevel.Id);
        }
    }

    [Fact]
    public void Update_Name_DoesNotUpdatesOthers()
    {
        User entityUpdated;
        User entity1;
        User entity2;

        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new UsersRepository(context))
            {
                entity1 = context.Users.Skip(0).First();
                entityUpdated = context.Users.Skip(1).First();
                entity2 = context.Users.Skip(2).First();

                entityUpdated.Name = "New Name";
                repo.Update(entityUpdated);
            }
        }

        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            var entity1_after = context.Users.Where(e => e.Id.Equals(entity1.Id)).Single();
            entity1_after.Should().BeEquivalentTo(entity1);

            var entity2_after = context.Users.Where(e => e.Id.Equals(entity2.Id)).Single();
            entity2_after.Should().BeEquivalentTo(entity2);
        }
    }
}