namespace T_Database.T_UsersRepository;

[Collection("RepositoriesTests")]
public partial class T_Add: LocalAuthDatabaseTest
{
    [Fact]
    public void Add_GivenEntity_AddsThatEntity()
    {
        User entity;

        using (var context = new LocalAuthContextTest(Key))
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
                    PasswordHashed = "password"
                };

                repo.Add(entity);
                repo.SaveAsync();
            }
        }

        using (var context = new LocalAuthContextTest(Key))
        {
            context.Users.Should().Contain(entity);
        }
    }

    [Fact]
    public void Add_GivenEntity_AddsToSet()
    {
        User entity;

        using (var context = new LocalAuthContextTest(Key))
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
                    PasswordHashed = "password"
                };

                repo.Add(entity);
                repo.SaveAsync();
            }
        }

        using (var context = new LocalAuthContextTest(Key))
        {
            context.Users.Should().HaveCount(3);
        }
    }
}

public partial class T_Add
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
            AccessLevel = new AccessLevel { Id = Guid.NewGuid(), Value = Database.Models.Enums.AccessLevels.Admin }
        });
        context.SaveChanges();
    }
}