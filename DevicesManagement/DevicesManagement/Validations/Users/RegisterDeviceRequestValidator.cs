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

        RuleFor(request => request.Address).NotNull()
            .Matches(IPv4ValidationUtils.REGEX_PATTERN)
            .Must(BeIPv4);
    }

    protected bool BeIPv4(string? address)
        => address is not null && IPv4ValidationUtils.IsValid(address);
}
