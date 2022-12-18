namespace T_Database.T_UsersRepository;

public class T_Add: LocalAuthDatabaseTest
{
    public T_Add() : base("Add") { }

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
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Admin }
        });
        context.SaveChanges();
    }


    [Fact]
    public void Add_GivenEntity_AddsThatEntity()
    {
        User entity;

        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new UsersRepository(context))
            {
                entity = new User
                {
                    CreatedDate = DateTime.Now,
                    Name = "newly created user",
                    UpdatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    EmployeeId = "eid 3",
                    AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Employee },
                    Password = "password"
                };

                repo.Add(entity);
            }
        }

        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            context.Users.Should().Contain(entity);
        }
    }

    [Fact]
    public void Add_GivenEntity_AddsToSet()
    {
        User entity;

        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            EnsureClear(context);
            Seed(context);

            using (var repo = new UsersRepository(context))
            {
                entity = new User
                {
                    CreatedDate = DateTime.Now,
                    Name = "newly created user",
                    UpdatedDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    EmployeeId = "eid 3",
                    AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Employee },
                    Password = "password"
                };

                repo.Add(entity);
            }
        }

        using (var context = new LocalAuthContextTest(ContextOptions))
        {
            context.Users.Should().HaveCount(3);
        }
    }
}