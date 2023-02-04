using Database.Models;
using Database.Repositories;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Commands;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Command, CommandsRepository, RunCommandCommand>),
    order: 1
)]
public record RunCommandCommand : IRequest<IActionResult>, IResourceAuthorizableCommand<Command>
{
    public Guid ResourceId { get; init; }
    public Command Resource { get; set; }
}
