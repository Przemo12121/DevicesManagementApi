using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, DevicesRepository, DeleteDeviceCommand, DeviceDto>),
    order: 1
)]
public class DeleteDeviceCommand : IRequest<DeviceDto>, IResourceAuthorizableCommand<Device>
{
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
}
