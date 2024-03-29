﻿using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Commands;

public class UpdateCommandRequestValidator : AbstractValidator<UpdateCommandRequest>
{
    public UpdateCommandRequestValidator()
    {
        RuleFor(request => request).Must(ValidationUtils.Common.IsNotEmpty);

        RuleFor(request => request.Name)
            .Length(1, 64);

        RuleFor(request => request.Body)
            .Length(1, 2048);

        RuleFor(request => request.Description)
            .MaximumLength(4096);
    }
}
