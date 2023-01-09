using Database.Models;
using Database.Repositories;
using DevicesManagement.MediatR.PipelineBehaviors.Authorization;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Commands;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Command, CommandsRepository, RunCommandCommand, string>),
    order: 1
)]
public class RunCommandCommand : IRequest<string>, IResourceAuthorizableCommand<Command>
{
    public Guid ResourceId { get; init; }
    public Command Resource { get; set; }
}
