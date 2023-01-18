using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Devices;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterDeviceRequest, RegisterDeviceRequestValidator, RegisterDeviceCommand, DeviceDto>),
    order: 1
)]
public class RegisterDeviceCommand : IRequest<DeviceDto>, IRequestContainerCommand<RegisterDeviceRequest>
{
    public RegisterDeviceRequest Request { get; init; }
}
