using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Devices;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, DevicesRepository, UpdateDeviceCommand, DeviceDto>),
    order: 1
)]
[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<UpdateDeviceRequest, UpdateDeviceRequestValidator, UpdateDeviceCommand, DeviceDto>),
    order: 2
)]
public class UpdateDeviceCommand : IRequest<DeviceDto>, IRequestContainerCommand<UpdateDeviceRequest>, IResourceAuthorizableCommand<Device>
{
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
    public UpdateDeviceRequest Request { get; init; }
}
