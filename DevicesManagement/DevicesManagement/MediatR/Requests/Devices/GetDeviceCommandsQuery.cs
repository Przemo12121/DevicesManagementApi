using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, DevicesRepository, GetDeviceCommandsQuery>),
    order: 1
)]
[MediatRBehavior(
    typeof(ListDeviceCommandValidationPipelineBehavior),
    order: 2
)]
public class GetDeviceCommandsQuery : IRequest<IActionResult>, IRequestContainerCommand<PaginationRequest>, IResourceAuthorizableCommand<Device>
{
    public PaginationRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
}
