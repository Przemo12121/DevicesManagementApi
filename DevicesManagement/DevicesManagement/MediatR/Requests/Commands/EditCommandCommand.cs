using Database.Models;
using MediatR;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR.Extensions.AttributedBehaviors;
using DevicesManagement.Validations.Commands;
using DevicesManagement.MediatR.PipelineBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Commands;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<EditCommandRequest, EditCommandCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Command, EditCommandCommand>),
    order: 2
)]
public record EditCommandCommand 
    : IRequest<IActionResult>, IResourceAuthorizableCommand<Command>, IRequestContainerCommand<EditCommandRequest>
{
    public Guid ResourceId { get; init; }
    public Command Resource { get; set; }
    public EditCommandRequest Request { get; init; }
}
