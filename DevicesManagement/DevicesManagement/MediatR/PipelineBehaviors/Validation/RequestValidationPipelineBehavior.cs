using FluentValidation;
using MediatR;

namespace DevicesManagement.MediatR.PipelineBehaviors.Validation;

public class RequestValidationPipelineBehavior<T, TValidator, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TValidator : IValidator<T>
    where TRequest : IRequest<TResponse>
{
    protected IEnumerable<IValidator<T>> Validators { get; init; }

    public RequestValidationPipelineBehavior(IEnumerable<IValidator<T>> validators)
    {
        Validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }
    public RequestValidationPipelineBehavior(IValidator<T> validator)
    {
        if (validator == null) throw new ArgumentNullException(nameof(validator));

        Validators = new List<IValidator<T>>();
        Validators.Append(validator);
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
