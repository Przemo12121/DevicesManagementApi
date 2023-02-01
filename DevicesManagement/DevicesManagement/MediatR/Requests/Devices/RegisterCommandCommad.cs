using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Devices;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, DevicesRepository, RegisterCommandCommand, CommandDto>),
    order: 1
)]
[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterCommandRequest, RegisterCommandRequestValidator, RegisterCommandCommand, CommandDto>),
    order: 2
)]
public class RegisterCommandCommand : IRequest<CommandDto>, IRequestContainerCommand<RegisterCommandRequest>, IResourceAuthorizableCommand<Device>
{

    public RegisterCommandRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
}
