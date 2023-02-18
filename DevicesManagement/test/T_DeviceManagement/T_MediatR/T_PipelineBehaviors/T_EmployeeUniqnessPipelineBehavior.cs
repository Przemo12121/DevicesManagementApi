namespace T_DeviceManagement.T_MediatR.T_PipelineBehaviors;

public class T_EmployeeUniqnessPipelineBehavior
{
    static Task<IActionResult> DummyDelegateMethod() => Task.FromResult((IActionResult)new OkObjectResult("dummy"));
    CancellationToken DummyCancellationToken { get; } = new();

    [Fact]
    public async void Handle_GivenNonNullableEmployeeId_Returns200()
    {
        var repositoryMock = new Mock<IUsersRepository>();
        repositoryMock.Setup(repository => repository.FindByEmployeeIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<User?>(new User()));

        var request = new DummyEmployeeIdContainerRequest()
        {
            Request = new()
            {
                EmployeeId = null
            }
        };

        var pipeline = new EmployeeIdUniquenessPipelineBehavior<
            DummyEmployeeIdRequest, 
            DummyEmployeeIdContainerRequest
        >(repositoryMock.Object);

        var result = await pipeline.Handle(request, DummyDelegateMethod, DummyCancellationToken);

        result.As<ObjectResult>()
            .StatusCode
            .Should()
            .Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async void Handle_GivenNullableEmployeeId_ReturnsDelegateResponse()
    {
        var repositoryMock = new Mock<IUsersRepository>();
        repositoryMock.Setup(repository => repository.FindByEmployeeIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<User?>(new User()));
        var request = new DummyEmployeeIdContainerRequest()
        {
            Request = new()
            {
                EmployeeId = null
            }
        };

        var pipeline = new EmployeeIdUniquenessPipelineBehavior<
            DummyEmployeeIdRequest,
            DummyEmployeeIdContainerRequest
        >(repositoryMock.Object);

        var result = await pipeline.Handle(request, DummyDelegateMethod, DummyCancellationToken);

        result.As<ObjectResult>()
            .Value
            .Should()
            .Be("dummy");
    }

    [Fact]
    public async void Handle_GivenTakenEmployeeId_Returns409()
    {
        var repositoryMock = new Mock<IUsersRepository>();
        repositoryMock.Setup(repository => repository.FindByEmployeeIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<User?>(new User()));
        var request = new DummyEmployeeIdContainerRequest()
        {
            Request = new()
            {
                EmployeeId = "abcd12345678"
            }
        };

        var pipeline = new EmployeeIdUniquenessPipelineBehavior<
            DummyEmployeeIdRequest,
            DummyEmployeeIdContainerRequest
        >(repositoryMock.Object);

        var result = await pipeline.Handle(request, DummyDelegateMethod, DummyCancellationToken);

        result.As<ObjectResult>()
            .StatusCode
            .Should()
            .Be(StatusCodes.Status409Conflict);
    }

    [Fact]
    public async void Handle_GivenNonTakenEmployeeId_Returns200()
    {
        var repositoryMock = new Mock<IUsersRepository>();
        repositoryMock.Setup(repository => repository.FindByEmployeeIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<User?>(null));

        var request = new DummyEmployeeIdContainerRequest()
        {
            Request = new()
            {
                EmployeeId = "abcd12345678"
            }
        };

        var pipeline = new EmployeeIdUniquenessPipelineBehavior<
            DummyEmployeeIdRequest,
            DummyEmployeeIdContainerRequest
        >(repositoryMock.Object);

        var result = await pipeline.Handle(request, DummyDelegateMethod, DummyCancellationToken);

        result.As<ObjectResult>()
            .StatusCode
            .Should()
            .Be(StatusCodes.Status200OK);
    }
}