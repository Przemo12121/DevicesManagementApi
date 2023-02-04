using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<User, RegisterDeviceCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterDeviceRequest, RegisterDeviceCommand>),
    order: 2
)]
public class RegisterDeviceCommand 
    : IRequest<IActionResult>, IRequestContainerCommand<RegisterDeviceRequest>, IResourceAuthorizableCommand<User>
{
    public RegisterDeviceRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public User Resource { get; set; }
}
