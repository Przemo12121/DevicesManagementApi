using DevicesManagement.MediatR.Commands;
using DevicesManagement.DataTransferObjects.Responses;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace DevicesManagement.MediatR.PipelineBehaviors.Validation;

public class RequestValidationPipelineBehavior<T, TValidator, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TValidator : IValidator<T>
    where TRequest : IRequest<TResponse>, IRequestContainerCommand<T>
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
        var result = Validate(request.Request);
        if (!result.IsValid)
        {
            throw new BadRequestResponse(GroupErrorsByProperty(result.Errors));
        }

        throw new NotImplementedException();
    }

    protected ValidationResult Validate(T request)
    {
        var errors = Validators.SelectMany(validator => validator.Validate(request).Errors)
            .Where(errors => errors != null)
            .ToList();

        return new ValidationResult(!errors.Any(), errors);
    }

    protected record ValidationResult(bool IsValid, IEnumerable<ValidationFailure> Errors);

    protected IEnumerable<KeyValuePair<string, IEnumerable<string>>> GroupErrorsByProperty(IEnumerable<ValidationFailure> errors)
        => errors.GroupBy(x => x.PropertyName)
            .Select(grouped => KeyValuePair.Create(
                grouped.First().PropertyName,
                grouped.Select(x => x.ErrorMessage)
            ));
}
