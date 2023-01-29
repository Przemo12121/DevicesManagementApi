using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace DevicesManagement.Validations.Devices;

public class UpdateDeviceRequestValidator : AbstractValidator<UpdateDeviceRequest>
{
    public UpdateDeviceRequestValidator()
    {
        RuleFor(request => request).Must(NotBeEmpty);
        RuleFor(request => request.Name).Length(1, 256);
        RuleFor(request => request.Address).Matches(IPv4ValidationUtils.REGEX_PATTERN).Must(BeIPv4);
    }

    protected bool NotBeEmpty(UpdateDeviceRequest request)
        => !request.Address.IsNullOrEmpty() || !request.Name.IsNullOrEmpty();

    protected bool BeIPv4(string? address)
        => address is not null
            ? IPv4ValidationUtils.IsValid(address)
            : true;
}
