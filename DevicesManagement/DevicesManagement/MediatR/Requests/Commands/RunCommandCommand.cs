using Database.Models;
using DevicesManagement.MediatR.Commands;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Requests.Commands;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Command, RunCommandCommand>),
    order: 1
)]
public record RunCommandCommand 
    : IRequest<IActionResult>, IResourceAuthorizableCommand<Command>
{
    public Guid ResourceId { get; init; }
    public Command Resource { get; set; }
}
