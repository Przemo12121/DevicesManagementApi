using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Devices;

public class UpdateDeviceRequestValidator : AbstractValidator<UpdateDeviceRequest>
{
    public UpdateDeviceRequestValidator()
    {
        RuleFor(request => request)
            .Must(ValidationUtils.Common.IsNotEmpty);
        
        RuleFor(request => request.Name)
            .Length(1, 256);
        
        RuleFor(request => request.Address!)
            .Must(ValidationUtils.IPv4.IsValid)
            .When(request => request.Address is not null);
    }
}
