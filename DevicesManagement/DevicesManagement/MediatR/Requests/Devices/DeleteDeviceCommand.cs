using Database.Models;
using DevicesManagement.MediatR.Commands;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Requests.Commands;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, DeleteDeviceCommand>),
    order: 1
)]
public class DeleteDeviceCommand
    : IRequest<IActionResult>, IResourceAuthorizableCommand<Device>
{
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
}
