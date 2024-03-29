﻿using DevicesManagement.Errors;
using DevicesManagement.MediatR.Commands;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.PipelineBehaviors;

public class RequestValidationPipelineBehavior<T, TRequest> : IPipelineBehavior<TRequest, IActionResult>
    where TRequest : IRequest<IActionResult>, IRequestContainerCommand<T>
{
    private readonly IEnumerable<IValidator<T>> _valdiators;

    public RequestValidationPipelineBehavior(IEnumerable<IValidator<T>> validators)
    {
        _valdiators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<IActionResult> Handle(TRequest request, RequestHandlerDelegate<IActionResult> next, CancellationToken cancellationToken)
    {
        var result = Validate(request.Request);
        if (!result.IsValid)
        {
            return ErrorResponses.CreatValidationFailures(
                GroupErrorsByProperty(result.Errors)
            );
        }

        return await next();
    }

    protected ValidationResult Validate(T request)
    {
        var errors = _valdiators.Select(validator => validator.Validate(request))
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .ToList();

        return new ValidationResult(!errors.Any(), errors);
    }

    protected record ValidationResult(bool IsValid, IEnumerable<ValidationFailure> Errors);

    protected IDictionary<string, string[]> GroupErrorsByProperty(IEnumerable<ValidationFailure> errors)
        => errors.GroupBy(x => x.PropertyName).ToDictionary(
                grouped => grouped.First().PropertyName,
                grouped => grouped.Select(x => x.ErrorMessage).ToArray()
            );
}
