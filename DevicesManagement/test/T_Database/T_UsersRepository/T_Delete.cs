namespace T_Database.T_UsersRepository;

public partial class T_Delete : LocalAuthDatabaseTest
{
    [Fact]
    public void Delete_GivenEntity_DeletesThatEntity()
    {
        User entity;

        using (var context = new LocalAuthContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new UsersRepository(context))
            {
                entity = context.Users.Skip(1).First();

                repo.Delete(entity);
                repo.SaveChanges();
            }
        }

        using (var context = new LocalAuthContextTest(Key))
        {
            context.Users.Should().NotContain(entity);
        }
    }

    [Fact]
    public void Delete_GivenEntity_DeletesEntityFromSet()
    {
        using (var context = new LocalAuthContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new UsersRepository(context))
            {
                var entity = context.Users.Skip(1).First();

                repo.Delete(entity);
                repo.SaveChanges();
            }
        }

        using (var context = new LocalAuthContextTest(Key))
        {
            context.Users.Should().HaveCount(2);
        }
    }

    [Fact]
    public void Delete_GivenEntity_DoesNotModifyOthers()
    {
        User deleted;
        User entity1;
        User entity2;

        using (var context = new LocalAuthContextTest(Key))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new UsersRepository(context))
            {
                entity1 = context.Users.Skip(0).First();
                deleted = context.Users.Skip(1).First();
                entity2 = context.Users.Skip(2).First();

                repo.Delete(deleted);
                repo.SaveChanges();
            }
        }

        using (var context = new LocalAuthContextTest(Key))
        {
            var entity1_after = context.Users.Skip(0).First();
            entity1_after.Should().BeEquivalentTo(entity1);
            
            var entity2_after = context.Users.Skip(1).First();
            entity2_after.Should().BeEquivalentTo(entity2);
        }
    }
}

public partial class T_Delete
{
    public T_Delete() : base("Delete") { }

    private void Seed(LocalAuthContextTest context)
    {
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Admin }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 2",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 2",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Employee }
        });
        context.Users.Add(new User
        {
            CreatedDate = DateTime.Now,
            Name = "dummy user 3",
            UpdatedDate = DateTime.Now,
            Id = Guid.NewGuid(),
            EmployeeId = "some id 3",
            PasswordHashed = "password",
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Admin }
        });
        context.SaveChanges();
    }
}