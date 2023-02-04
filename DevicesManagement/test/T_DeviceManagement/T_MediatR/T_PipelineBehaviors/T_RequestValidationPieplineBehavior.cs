namespace T_DeviceManagement.T_MediatR.T_PipelineBehaviors;

public partial class T_RequestValidationPieplineBehavior
{

    [Fact]
    public async void Handle_SuccessfulValidation_NotThrowsException()
    {
        var alwaysPassingValidator = new Mock<IValidator<DummyRequest>>();
        alwaysPassingValidator.Setup(validator => validator.Validate(It.IsAny<DummyRequest>()))
            .Returns(SuccessResult);

        var pipeline = new RequestValidationPipelineBehavior<DummyRequest, IValidator<DummyRequest>, DummyValidableRequestCommand>(
            new List<IValidator<DummyRequest>>() { alwaysPassingValidator.Object }
        );

        await pipeline.Invoking(pipe => pipe.Handle(new DummyValidableRequestCommand(), DummyDelegateMethod, DummyCancellationToken))
            .Should()
            .NotThrowAsync();
    }

    [Fact]
    public async void Handle_SuccessfulValidation_ReturnsDelegateResult()
    {
        var alwaysPassingValidator = new Mock<IValidator<DummyRequest>>();
        alwaysPassingValidator.Setup(validator => validator.Validate(It.IsAny<DummyRequest>()))
            .Returns(SuccessResult);

        var pipeline = new RequestValidationPipelineBehavior<DummyRequest, IValidator<DummyRequest>, DummyValidableRequestCommand>(
            new List<IValidator<DummyRequest>>() { alwaysPassingValidator.Object }
        );

        var result = await pipeline.Handle(new DummyValidableRequestCommand(), DummyDelegateMethod, DummyCancellationToken);

        result.As<OkObjectResult>()
            .Value
            .Should()
            .Be("dummy");
    }

    [Fact]
    public async void Handle_UnsuccessfulValidation_NotThrowsException()
    {
        var alwaysFailingValidator = new Mock<IValidator<DummyRequest>>();
        alwaysFailingValidator.Setup(validator => validator.Validate(It.IsAny<DummyRequest>()))
            .Returns(UnsuccessfulResult);

        var pipeline = new RequestValidationPipelineBehavior<DummyRequest, IValidator<DummyRequest>, DummyValidableRequestCommand>(
            new List<IValidator<DummyRequest>>() { alwaysFailingValidator.Object }
        );

        await pipeline.Invoking(pipe => pipe.Handle(new DummyValidableRequestCommand(), DummyDelegateMethod, DummyCancellationToken))
            .Should()
            .NotThrowAsync<Exception>();
    }

    [Fact]
    public async void Handle_UnsuccessfulValidation_ReturnsBadRequestResult()
    {
        var alwaysFailingValidator = new Mock<IValidator<DummyRequest>>();
        alwaysFailingValidator.Setup(validator => validator.Validate(It.IsAny<DummyRequest>()))
            .Returns(UnsuccessfulResult);

        var pipeline = new RequestValidationPipelineBehavior<DummyRequest, IValidator<DummyRequest>, DummyValidableRequestCommand>(
            new List<IValidator<DummyRequest>>() { alwaysFailingValidator.Object }
        );

        var result = await pipeline.Handle(new DummyValidableRequestCommand(), DummyDelegateMethod, DummyCancellationToken);
        result.As<BadRequestObjectResult>()
            .StatusCode
            .Should()
            .Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async void Handle_OneUnsuccessfulValidationFromMany_ReturnsBadRequestResult()
    {
        var alwaysFailingValidator = new Mock<IValidator<DummyRequest>>();
        alwaysFailingValidator.Setup(validator => validator.Validate(It.IsAny<DummyRequest>()))
            .Returns(UnsuccessfulResult);
        var alwaysSuccessfulValidator = new Mock<IValidator<DummyRequest>>();
        alwaysSuccessfulValidator.Setup(validator => validator.Validate(It.IsAny<DummyRequest>()))
            .Returns(SuccessResult);

        var pipeline = new RequestValidationPipelineBehavior<DummyRequest, IValidator<DummyRequest>, DummyValidableRequestCommand>(
            new List<IValidator<DummyRequest>>() { alwaysSuccessfulValidator.Object, alwaysFailingValidator.Object }
        );

        var result = await pipeline.Handle(new DummyValidableRequestCommand(), DummyDelegateMethod, DummyCancellationToken);
        result.As<BadRequestObjectResult>()
            .StatusCode
            .Should()
            .Be(StatusCodes.Status400BadRequest);
    }
}

public partial class T_RequestValidationPieplineBehavior
{
    static Task<IActionResult> DummyDelegateMethod() => Task.FromResult((IActionResult)new OkObjectResult("dummy"));
    CancellationToken DummyCancellationToken { get; } = new();

    ValidationResult SuccessResult { get; } = new()
    {
        Errors = new ()
    };

    ValidationResult UnsuccessfulResult { get; } = new()
    {
        Errors = new()
        {
            new ValidationFailure()
            {
                ErrorMessage = "Dummy error message",
                PropertyName = "DummyProperty"
            }
        }
    };
}
