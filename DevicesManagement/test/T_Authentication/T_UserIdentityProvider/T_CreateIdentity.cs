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
        DummyUser.PasswordHashed = new PasswordHasher<User>().HashPassword(DummyUser, "dummyPASSWORD1");

        var usersRepositoryMock = new Mock<IUsersRepository<User>>();

        usersRepositoryMock.Setup(mock => mock.FindByEmployeeId("abcd12345678"))
            .Returns(DummyUser);

        usersRepositoryMock.Setup(mock => mock.FindByEmployeeId("badx12345678"))
            .Returns((User?)null);

        Provider = new UserIdentityProvider(usersRepositoryMock.Object);
    }

    [Fact]
    public void Identify_WithValidCredentials_ReturnsDummyUser()
    {
        var user = Provider.Identify("abcd12345678", "dummyPASSWORD1");

        user.Should().NotBeNull();
        user.Should().Be(DummyUser);
    }

    [Fact]
    public void Identify_WithInvalidEmployeeId_ReturnsNull()
    {
        var user = Provider.Identify("badx12345678", "dummyPASSWORD1");

        user.Should().BeNull();
    }

    [Fact]
    public void Identify_WithInvalidPassword_ReturnsNull()
    {
        var user = Provider.Identify("abcd12345678", "badPASSWORD1");

        user.Should().BeNull();
    }
}