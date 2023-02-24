using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Users;

public class RegisterDeviceRequestValidator : AbstractValidator<RegisterDeviceRequest>
{
    public RegisterDeviceRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotNull()
            .Length(1, 256);

        RuleFor(request => request.Address)
            .NotNull()
            .DependentRules(() =>
                RuleFor(request => request.Address)
                    .Must(ValidationUtils.IPv4.IsValid)

            );
    }
}
