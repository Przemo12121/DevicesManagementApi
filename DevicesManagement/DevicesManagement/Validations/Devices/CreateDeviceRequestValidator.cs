using DevicesManagement.DataTransferObjects.Requests;
using FluentValidation;

namespace DevicesManagement.Validations.Devices;

public class CreateDeviceRequestValidator : AbstractValidator<CreateDeviceRequest>
{
    public CreateDeviceRequestValidator()
    {
        RuleFor(request => request.Name).NotNull().Length(1, 256);
        RuleFor(request => request.Address).NotNull().Matches("^(0|[1-9][0-9]{0,2})(\\.(0|[1-9][0-9]{0,2})){3}:(0|[1-9][0-9]*)$").Must(BeIPv4);
    }

    protected bool BeIPv4(string? address)
    {
        return address?.Split(':')
            .First()?
            .Split('.')
            .Select(value => Int16.Parse(value))
            .All(value => value <= 255) // regex protects from numbers lower than 0
            ?? false;
    }
}
