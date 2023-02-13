﻿namespace IntegrationTests.Users;

public partial class RegisterEmployee : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<BaseSetup>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly BaseSetup _setupFixture;

    HttpClient HttpClient { get; init; }
    User RequestingUser { get; init; }
    string RequestingUserJwt { get; init; }

    string Route { get; } = "api/users/employees";

    RegisterEmployeeRequest DummyRequest { get; } = new()
    {
        EmployeeId = "test11100123",
        Name = "Dummy employee",
        Password = "dummyPASSword991"
    };

    User ExistingEmployee { get; init; }

    public RegisterEmployee(WebApplicationFactory<Program> factory, BaseSetup setupFixture)
    {
        _factory = factory;
        _setupFixture = setupFixture;

        HttpClient = _factory.CreateClient();
        RequestingUser = setupFixture.RequestingUser;
        RequestingUserJwt = factory.Services.GetRequiredService<IJwtProvider>().Generate(RequestingUser).RawData;

        using var context = new LocalAuthStorageContext();
        ExistingEmployee = new()
        {
            AccessLevelId = _setupFixture.AdminAccessLevel.Id,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            EmployeeId = "aaaa11111111",
            Name = "existing user",
            PasswordHashed = "not important"
        };
        context.Users.Add(ExistingEmployee);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = new LocalAuthStorageContext();
        context.Users.RemoveRange(
            context.Users.Where(
                d => d.EmployeeId.Equals(DummyRequest.EmployeeId) && d.Name.Equals(DummyRequest.Name)
            )
        );
        context.Users.Remove(ExistingEmployee);
        context.SaveChanges();

        GC.SuppressFinalize(this);
    }
}