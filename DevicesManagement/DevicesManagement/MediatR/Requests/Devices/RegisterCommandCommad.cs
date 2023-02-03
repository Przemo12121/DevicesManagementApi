using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Devices;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<Device, DevicesRepository, RegisterCommandCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterCommandRequest, RegisterCommandRequestValidator, RegisterCommandCommand>),
    order: 2
)]
public class RegisterCommandCommand : IRequest<IActionResult>, IRequestContainerCommand<RegisterCommandRequest>, IResourceAuthorizableCommand<Device>
{

    public RegisterCommandRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public Device Resource { get; set; }
}
