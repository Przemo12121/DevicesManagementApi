namespace T_DeviceManagement.T_MediatR.T_PipelineBehaviors;

public partial class T_ResourceAuthorizationPieplineBehavior
{

    [Fact]
    public async void Handle_AdminRequestingExistingResource_NotThrowsException()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource, 
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedAdminHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = OwnerResource.Id
        };

        await pipeline.Invoking(pipe => pipe.Handle(command, DummyDelegateMethod, DummyCancellationToken))
            .Should()
            .NotThrowAsync<Exception>();
    }

    [Fact]
    public async void Handle_AdminRequestingExistingResource_ModifiesCommandToCarryTheResource()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedAdminHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = OwnerResource.Id
        };

        await pipeline.Handle(command, DummyDelegateMethod, DummyCancellationToken);

        command.Resource.Should().Be(OwnerResource);
    }

    [Fact]
    public async void Handle_AdminRequestingExistingResource_ReturnsDelegateResult()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedAdminHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = OwnerResource.Id
        };

        var result = await pipeline.Handle(command, DummyDelegateMethod, DummyCancellationToken);

        result.As<OkObjectResult>()
            .Value
            .Should()
            .Be("dummy");
    }

    [Fact]
    public async void Handle_AdminRequestingNonexistingResource_NotThrowsException()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedAdminHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = Guid.NewGuid()
        };

        await pipeline.Invoking(pipe => pipe.Handle(command, DummyDelegateMethod, DummyCancellationToken))
            .Should()
            .NotThrowAsync<Exception>();
    }

    [Fact]
    public async void Handle_AdminRequestingNonexistingResource_ReturnsNotFoundException()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedAdminHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = Guid.NewGuid()
        };

        var result = await pipeline.Handle(command, DummyDelegateMethod, DummyCancellationToken);

        result.As<NotFoundObjectResult>()
            .StatusCode
            .Should()
            .Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async void Handle_OwnerRequestingHisResource_NotThrowsException()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedOwnerHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = OwnerResource.Id
        };

        await pipeline.Invoking(pipe => pipe.Handle(command, DummyDelegateMethod, DummyCancellationToken))
            .Should()
            .NotThrowAsync<Exception>();
    }

    [Fact]
    public async void Handle_OwnerRequestingHisResource_ModifiesCommandToCarryTheResource()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedOwnerHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = OwnerResource.Id
        };

        await pipeline.Handle(command, DummyDelegateMethod, DummyCancellationToken);

        command.Resource.Should().Be(OwnerResource);
    }

    [Fact]
    public async void Handle_OwnerRequestingHisResource_ReturnsDelegateResult()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedOwnerHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = OwnerResource.Id
        };

        var result = await pipeline.Handle(command, DummyDelegateMethod, DummyCancellationToken);

        result.As<OkObjectResult>()
            .Value
            .Should()
            .Be("dummy");
    }

    [Fact]
    public async void Handle_OwnerRequestingNotHisResource_NotThrowsException()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedOwnerHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = NonownerResource.Id
        };

        await pipeline.Invoking(pipe => pipe.Handle(command, DummyDelegateMethod, DummyCancellationToken))
            .Should()
            .NotThrowAsync<Exception>();
    }

    [Fact]
    public async void Handle_OwnerRequestingNotHisResource_ReturnsNotFoundResult()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedOwnerHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = NonownerResource.Id
        };

        var result = await pipeline.Handle(command, DummyDelegateMethod, DummyCancellationToken);

        result.As<NotFoundObjectResult>()
            .StatusCode
            .Should()
            .Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async void Handle_OwnerRequestingNonExistingResource_ReturnsNotFoundResult()
    {
        var pipeline = new ResourceAuthorizationPipelineBehavior<
            DummyResource,
            DummyAuthorizableRequestCommand
        >(MockedRepository, MockedOwnerHttpContext);

        var command = new DummyAuthorizableRequestCommand()
        {
            ResourceId = Guid.NewGuid()
        };

        var result = await pipeline.Handle(command, DummyDelegateMethod, DummyCancellationToken);

        result.As<NotFoundObjectResult>()
            .StatusCode
            .Should()
            .Be(StatusCodes.Status404NotFound);
    }
}

public partial class T_ResourceAuthorizationPieplineBehavior
{
    #region dummy .Handle() requirements
    static Task<IActionResult> DummyDelegateMethod() => Task.FromResult((IActionResult)new OkObjectResult("dummy"));
    CancellationToken DummyCancellationToken { get; } = new();
    #endregion

    #region dummy users
    User Owner { get;  } = new()
    {
        Id = Guid.Parse("12345678-1234-1234-1234-123456123456"),
        EmployeeId = "abcd12345678",
        AccessLevel = new AccessLevel() { Value = AccessLevels.Employee }
    };
    User Admin { get; } = new()
    {
        Id = Guid.Parse("87654321-4321-4321-4321-654321654321"),
        EmployeeId = "dcba87654321",
        AccessLevel = new AccessLevel() { Value = AccessLevels.Admin }
    };
    #endregion

    #region dummy resources
    DummyResource OwnerResource = new()
    {
        Id = Guid.Parse("abcdefab-abcd-abcd-abcd-abcdefabcdef")
    };
    DummyResource NonownerResource = new()
    {
        Id = Guid.Parse("12345678-abcd-1234-abcd-123456abcdef")
    };
    #endregion

    IResourceAuthorizableRepository<DummyResource> MockedRepository { get; }
    IHttpContextAccessor MockedAdminHttpContext { get; }
    IHttpContextAccessor MockedOwnerHttpContext { get; }

    public T_ResourceAuthorizationPieplineBehavior()
    {
        var repositoryMock = new Mock<IResourceAuthorizableRepository<DummyResource>>();

        repositoryMock.Setup(repository => repository.FindById(It.IsAny<Guid>()))
            .Returns((DummyResource) null);
        repositoryMock.Setup(repository => repository.FindById(OwnerResource.Id))
            .Returns(OwnerResource);
        repositoryMock.Setup(repository => repository.FindById(NonownerResource.Id))
            .Returns(NonownerResource);

        repositoryMock.Setup(repository => repository.FindByIdAndOwnerId(It.IsAny<Guid>(), Owner.EmployeeId))
            .Returns((DummyResource) null);
        repositoryMock.Setup(repository => repository.FindByIdAndOwnerId(OwnerResource.Id, Owner.EmployeeId))
            .Returns(OwnerResource);
        repositoryMock.Setup(repository => repository.FindByIdAndOwnerId(NonownerResource.Id, Owner.EmployeeId))
            .Returns((DummyResource) null);

        MockedRepository = repositoryMock.Object;
        MockedAdminHttpContext = DummyHttpContextAccessorFactory.Create(Admin.EmployeeId, Admin.AccessLevel.Value.ToString());
        MockedOwnerHttpContext = DummyHttpContextAccessorFactory.Create(Owner.EmployeeId, Owner.AccessLevel.Value.ToString());
    }
}
