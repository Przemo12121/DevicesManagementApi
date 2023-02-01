using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, DevicesRepository,GetDeviceCommandsQuery, List<CommandDto>>),
    order: 1
)]
[MediatRBehavior(
    typeof(ListDeviceCommandValidationPipelineBehavior),
    order: 2
)]
public class GetDeviceCommandsQuery : IRequest<List<CommandDto>>, IRequestContainerCommand<PaginationRequest>, IResourceAuthorizableCommand<Device>
{
    public PaginationRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
}
