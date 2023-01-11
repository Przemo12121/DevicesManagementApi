using Database.Models;
using MediatR;
using DevicesManagement.DataTransferObjects.Responses.Commands;
using DevicesManagement.DataTransferObjects.Requests;
using Database.Repositories;
using DevicesManagement.MediatR.PipelineBehaviors.Authorization;
using MediatR.Extensions.AttributedBehaviors;
using DevicesManagement.MediatR.PipelineBehaviors.Validation;
using DevicesManagement.Validations.Commands;

namespace DevicesManagement.MediatR.Commands.Commands;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<EditCommandRequest, EditCommandRequestValidator, EditCommandCommand, CommandDto>),
    order: 1
)]
[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Command, CommandsRepository, RunCommandCommand, string>),
    order: 2
)]
public record EditCommandCommand : IRequest<CommandDto>, IResourceAuthorizableCommand<Command>, IRequestContainerCommand<EditCommandRequest>
{
    public Guid ResourceId { get; init; }
    public Command Resource { get; set; }
    public EditCommandRequest Request { get; init; }
}
