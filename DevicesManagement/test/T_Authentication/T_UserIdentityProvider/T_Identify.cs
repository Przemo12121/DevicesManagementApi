namespace T_Authentication.T_UserIdentityProvider;

public partial class T_Identify
{
    [Fact]
    public async void Identify_WithValidCredentials_ReturnsDummyUser()
    {
        var user = await Provider.Identify("abcd12345678", "dummyPASSWORD1");

        user.Should().NotBeNull();
        user.Should().Be(DummyUser);
    }

    [Fact]
    public async void Identify_WithInvalidEmployeeId_ReturnsNull()
    {
        var user = await Provider.Identify("badx12345678", "dummyPASSWORD1");

        user.Should().BeNull();
    }

    [Fact]
    public async void Identify_WithInvalidPassword_ReturnsNull()
    {
        var user = await Provider.Identify("abcd12345678", "badPASSWORD1");

        user.Should().BeNull();
    }
}

public partial class T_Identify
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
            AccessLevel = new AccessLevel()
            {
                Value = AccessLevels.Admin,
                Description = "Dummy",
                Id = new Guid()
            }
        };
        DummyUser.PasswordHashed = new PasswordHasher<User>().HashPassword(DummyUser, "dummyPASSWORD1");

        var usersRepositoryMock = new Mock<IUsersRepository>();

        usersRepositoryMock.Setup(mock => mock.FindByEmployeeIdAsync("abcd12345678"))
            .Returns(Task.FromResult<User?>(DummyUser));

        usersRepositoryMock.Setup(mock => mock.FindByEmployeeIdAsync("badx12345678"))
            .Returns(Task.FromResult<User?>(null));

        Provider = new UserIdentityProvider(usersRepositoryMock.Object);
    }
}