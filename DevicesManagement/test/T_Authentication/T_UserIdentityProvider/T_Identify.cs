using Database.Models;
using Database.Repositories.Interfaces;

namespace T_Authentication.T_UserIdentityProvider;

public class T_Identify
{
    private UserIdentityProvider Provider { get; init; }
    private User DummyUser { get; init; }
    public T_Identify()
    {
        DummyUser = new User()
        {
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Enabled = true,
            EmployeeId = "abcd12345678",
            Name = "Dummy Name",
            Id = new Guid(),
            AccessLevel = new AccessLevel() { 
                Value = AccessLevels.Admin, 
                Description = "Dummy",
                Id = new Guid()
            }
        };

        var usersRepositoryMock = new Mock<IUsersRepository<User>>();
        usersRepositoryMock.Setup(mock => mock.FindByEmployeeId("abcd12345678"))
            .Returns(DummyUser);
        usersRepositoryMock.Setup(mock => mock.FindByEmployeeId("xyzw12121212"))
            .Returns(null);

        Provider = new UserIdentityProvider(usersRepositoryMock.Object);
    }

    [Fact]
    public void Test1()
    {

    }
}