using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, RegisterCommandCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterCommandRequest, RegisterCommandCommand>),
    order: 2
)]
public class RegisterCommandCommand 
    : IRequest<IActionResult>, IRequestContainerCommand<RegisterCommandRequest>, IResourceAuthorizableCommand<Device>
{

    public RegisterCommandRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
}
