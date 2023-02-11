namespace IntegrationTests.Users;

public partial class RegisterEmployee
{
    [Fact]
    public async void RegisterEmployee_ValidRequest_ResponsesWith200()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(DummyRequest));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async void RegisterEmployee_ValidRequest_CreateNewUserInDatabase()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        int countBefore;
        using (var context = new LocalAuthStorageContext())
        {
            countBefore = context.Users.Count();
        }

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(DummyRequest));


        using (var context = new LocalAuthStorageContext())
        {
            context.Users.Should().HaveCount(countBefore + 1);
        }
    }

    [Fact]
    public async void RegisterEmployee_ValidRequest_NewlyCreatedUserHasEmployeeAccessLevel()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(DummyRequest));

        using var context = new LocalAuthStorageContext();
        var newUser = context.Users
            .Where(u => !u.Equals(ExistingEmployee) && !u.Equals(RequestingUser))
            .Include(u => u.AccessLevel)
            .First();
        newUser.AccessLevel.Value.Should().Be(Database.Models.Enums.AccessLevels.Employee);
    }

    [Fact]
    public async void RegisterEmployee_ValidRequest_NewlyCreatedUserHasRequestedName()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(DummyRequest));

        using var context = new LocalAuthStorageContext();
        var newUser = context.Users
            .Where(u => !u.Equals(ExistingEmployee) && !u.Equals(RequestingUser))
            .First();
        newUser.Name.Should().Be(DummyRequest.Name);
    }

    [Fact]
    public async void RegisterEmployee_ValidRequest_NewlyCreatedUserHasRequestedEmployeeId()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(DummyRequest));

        using var context = new LocalAuthStorageContext();
        var newUser = context.Users
            .Where(u => !u.Equals(ExistingEmployee) && !u.Equals(RequestingUser))
            .First();
        newUser.EmployeeId.Should().Be(DummyRequest.EmployeeId);
    }

    [Fact]
    public async void RegisterEmployee_ValidRequest_NewlyCreatedUserHasRequestedPasswordHashed()
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(DummyRequest));

        var passwordHasher = new PasswordHasher<User>();

        using var context = new LocalAuthStorageContext();
        var newUser = context.Users
            .Where(u => !u.Equals(ExistingEmployee) && !u.Equals(RequestingUser))
            .First();

        var passwordHasherResult = passwordHasher.VerifyHashedPassword(newUser, newUser.PasswordHashed, DummyRequest.Password);

        passwordHasherResult.Should().Be(PasswordVerificationResult.Success);
    }

    [Fact]
    public async void RegisterEmployee_RequestWithoutToken_ResponsesWith401()
    {
        var response = await HttpClient.PostAsync(Route, JsonContent.Create(DummyRequest));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async void RegisterEmployee_RequestWithoutToken_DoesNotCreateUser()
    {
        int countBefore;
        using (var context = new LocalAuthStorageContext())
        {
            countBefore = context.Users.Count();
        }

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(DummyRequest));

        using (var context = new LocalAuthStorageContext())
        {
            context.Users.Should().HaveCount(countBefore);
        }
    }

    [Fact]
    public async void RegisterEmployee_RequestedExistingEmployeeId_ResponsesWith409()
    {
        RegisterEmployeeRequest invalidRequest = new()
        {
            Name = "aaa",
            Password = "aaaaaBBBBB333",
            EmployeeId = ExistingEmployee.EmployeeId
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(invalidRequest));

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async void RegisterEmployee_RequestedExistingEmployeeId_DoesNotCreateNewUser()
    {
        int countBefore;
        using (var context = new LocalAuthStorageContext())
        {
            countBefore = context.Users.Count();
        }

        RegisterEmployeeRequest invalidRequest = new()
        {
            Name = "aaa",
            Password = "aaaaaBBBBB333",
            EmployeeId = ExistingEmployee.EmployeeId
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(invalidRequest));

        using (var context = new LocalAuthStorageContext())
        {
            context.Users.Should().HaveCount(countBefore);
        }
    }

    [Fact]
    public async void RegisterEmployee_BadRequest_ResponsesWith400()
    {
        RegisterEmployeeRequest invalidRequest = new()
        {
            Name = "aaa",
            Password = "aaaaaBBBBB333",
            //EmployeeId = ExistingEmployee.EmployeeId
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(invalidRequest));
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void RegisterEmployee_BadRequest_DoesNotCreateNewUser()
    {
        int countBefore;
        using (var context = new LocalAuthStorageContext())
        {
            countBefore = context.Users.Count();
        }

        RegisterEmployeeRequest invalidRequest = new()
        {
            Name = "aaa",
            Password = "aaaaaBBBBB333",
            //EmployeeId = ExistingEmployee.EmployeeId
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RequestingUserJwt);

        var response = await HttpClient.PostAsync(Route, JsonContent.Create(invalidRequest));

        using (var context = new LocalAuthStorageContext())
        {
            context.Users.Should().HaveCount(countBefore);
        }
    }
}
