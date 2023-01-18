using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Devices;

public class RegisterCommandRequestValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandRequestValidator()
    {
        RuleFor(request => request.Name).NotNull().Length(1, 64);
        RuleFor(request => request.Body).NotNull().Length(1, 2048);
        RuleFor(request => request.Description).MaximumLength(4096);
    }
}
