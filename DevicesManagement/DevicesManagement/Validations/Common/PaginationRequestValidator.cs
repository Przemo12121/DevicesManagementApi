﻿using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Common;

public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationRequestValidator(int maxLimit, string[] orderKeys)
    {
        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);

        RuleFor(request => request.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(maxLimit);

        var alternativeOrderKeys = orderKeys.Aggregate((a, b) => a + '|' + b);
        RuleFor(request => request.Order!.ToLower())
            .Matches($"({alternativeOrderKeys}):(asc|desc)")
            .When(request => request.Order is not null);
    }
}
