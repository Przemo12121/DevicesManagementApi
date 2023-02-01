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

        // ?? "" added to workaounr .NotNull not failing validation for null property
        RuleFor(request => request.Address ?? "")
            .NotNull()
            .Must(ValidationUtils.IPv4.IsValid);
    }
}
