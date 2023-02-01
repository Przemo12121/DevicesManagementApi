using Database.Models;
using Database.Repositories;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Commands;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Command, CommandsRepository, DeleteCommandCommand, string>),
    order: 1
)]
public class DeleteCommandCommand : IRequest<string>, IResourceAuthorizableCommand<Command>
{
    public Guid ResourceId { get; init; }
    public Command Resource { get; set; }
}
