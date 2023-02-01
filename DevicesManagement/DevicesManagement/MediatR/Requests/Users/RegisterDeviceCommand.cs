using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Users;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<User, UsersRepository, RegisterDeviceCommand, DeviceDto>),
    order: 1
)]
[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterDeviceRequest, RegisterDeviceRequestValidator, RegisterDeviceCommand, DeviceDto>),
    order: 2
)]
public class RegisterDeviceCommand : IRequest<DeviceDto>, IRequestContainerCommand<RegisterDeviceRequest>, IResourceAuthorizableCommand<User>
{
    public RegisterDeviceRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public User Resource { get; set; }
}
