namespace T_Authentication.T_UserIdentityProvider;

public partial class T_CreateIdentity
{
    [Fact]
    public void CreateIdentity_GivenData_ReturnsUserWithGivenEmployeeId()
    {
        var user = Provider.CreateIdentity("abcd12345678", "dummyName", "dummyPASSWORD1", DummyAccessLevel);

        user.EmployeeId.Should().Be("abcd12345678");
    }

    [Fact]
    public void CreateIdentity_GivenData_ReturnsUserWithGivenName()
    {
        var user = Provider.CreateIdentity("abcd12345678", "dummyName", "dummyPASSWORD1", DummyAccessLevel);

        user.Name.Should().Be("dummyName");
    }

    [Fact]
    public void CreateIdentity_GivenData_ReturnsUserWithGivenAccessLevel()
    {
        var user = Provider.CreateIdentity("abcd12345678", "dummyName", "dummyPASSWORD1", DummyAccessLevel);

        user.AccessLevel.Should().Be(DummyAccessLevel);
    }

    [Fact]
    public void CreateIdentity_GivenData_ReturnsUserWithDifferentPasswordStringThanGiven()
    {
        var user = Provider.CreateIdentity("abcd12345678", "dummyName", "dummyPASSWORD1", DummyAccessLevel);

        user.PasswordHashed.Should().NotBe("dummyPASSWORD1");
    }

    [Fact]
    public void CreateIdentity_GivenData_ReturnsUserWithPasswordVerifiableByPasswordHasher()
    {
        var user = Provider.CreateIdentity("abcd12345678", "dummyName", "dummyPASSWORD1", DummyAccessLevel);

        var result = new PasswordHasher<User>()
            .VerifyHashedPassword(user, user.PasswordHashed, "dummyPASSWORD1");
        
        result.Should().Be(PasswordVerificationResult.Success);
    }

    [Fact]
    public void CreateIdentity_GivenData_ReturnsUserWithPasswordNotVerifiableByPasswordHasherWithOtherPassword()
    {
        var user = Provider.CreateIdentity("abcd12345678", "dummyName", "dummyPASSWORD1", DummyAccessLevel);

        var result = new PasswordHasher<User>()
            .VerifyHashedPassword(user, user.PasswordHashed, "otherPASSWORD1");

        result.Should().Be(PasswordVerificationResult.Failed);
    }
}

public partial class T_CreateIdentity
{
    private AccessLevel DummyAccessLevel { get; init; } = new AccessLevel()
    {
        Id = new Guid(),
        Value = AccessLevels.Admin,
        Description = "dummy description"
    };

    private UserIdentityProvider Provider { get; init; }
    public T_CreateIdentity()
    {
        var usersRepositoryMock = new Mock<IUsersRepository<User>>();
        Provider = new UserIdentityProvider(usersRepositoryMock.Object);
    }
}