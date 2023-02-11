using Database.Models;
using MediatR;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR.Extensions.AttributedBehaviors;
using DevicesManagement.MediatR.PipelineBehaviors;
using Microsoft.AspNetCore.Mvc;
using DevicesManagement.MediatR.Commands;

namespace DevicesManagement.MediatR.Requests.Commands;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<UpdateCommandRequest, UpdateCommandCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Command, UpdateCommandCommand>),
    order: 2
)]
public record UpdateCommandCommand 
    : IRequest<IActionResult>, IResourceAuthorizableCommand<Command>, IRequestContainerCommand<UpdateCommandRequest>
{
    public Guid ResourceId { get; init; }
    public Command Resource { get; set; }
    public UpdateCommandRequest Request { get; init; }
}
