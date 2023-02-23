using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<UpdateEmployeeRequest, UpdateCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<User, UpdateCommand>),
    order: 2
)]
[MediatRBehavior(
    typeof(EmployeeIdUniquenessPipelineBehavior<UpdateEmployeeRequest, UpdateCommand>),
    order: 3
)]
public class UpdateCommand 
    : IRequest<IActionResult>, IRequestContainerCommand<UpdateEmployeeRequest>, IResourceAuthorizableCommand<User>
{
    public UpdateEmployeeRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public User Resource { get; set; }
}
