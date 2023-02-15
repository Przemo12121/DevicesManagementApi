using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, GetCommandsQuery>),
    order: 1
)]
[MediatRBehavior(
    typeof(GetCommandsValidationPipelineBehavior),
    order: 2
)]
public class GetCommandsQuery 
    : IRequest<IActionResult>, IRequestContainerCommand<PaginationRequest>, IResourceAuthorizableCommand<Device>
{
    public PaginationRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
}
