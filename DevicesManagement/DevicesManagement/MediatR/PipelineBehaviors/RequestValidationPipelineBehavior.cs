using DevicesManagement.MediatR.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using DevicesManagement.Exceptions;

namespace DevicesManagement.MediatR.PipelineBehaviors;

public class RequestValidationPipelineBehavior<T, TValidator, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TValidator : IValidator<T>
    where TRequest : IRequest<TResponse>, IRequestContainerCommand<T>
{
    protected IEnumerable<IValidator<T>> Validators { get; init; }

    public RequestValidationPipelineBehavior(IEnumerable<IValidator<T>> validators)
    {
        Validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var result = Validate(request.Request);
        if (!result.IsValid)
        {
            throw new BadRequestHttpException(GroupErrorsByProperty(result.Errors));
        }

        return await next();
    }

    protected ValidationResult Validate(T request)
    {
        var errors = Validators.Select(validator => validator.Validate(request))
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .ToList();

        return new ValidationResult(!errors.Any(), errors);
    }

    protected record ValidationResult(bool IsValid, IEnumerable<ValidationFailure> Errors);

    protected IEnumerable<PropertyWithErrors> GroupErrorsByProperty(IEnumerable<ValidationFailure> errors)
        => errors.GroupBy(x => x.PropertyName)
                .Select(grouped => new PropertyWithErrors(
                    grouped.First().PropertyName,
                    grouped.Select(x => x.ErrorMessage)
                )
            );
}
