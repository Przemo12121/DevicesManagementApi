using DevicesManagement.DataTransferObjects;
using FluentValidation;

namespace DevicesManagement.Validations.Devices;

public class CreateDeviceRequestValidator : AbstractValidator<CreateDeviceRequest>
{
    public CreateDeviceRequestValidator()
    {
        RuleFor(request => request.Name).NotNull().Length(1, 256);
        RuleFor(request => request.Address).NotNull().Matches("^[0-9]{1,3}(\\.[0-9]){3}:[0-9]+$");
    }
}
