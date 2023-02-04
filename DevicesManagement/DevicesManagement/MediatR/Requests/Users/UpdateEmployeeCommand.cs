using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Users;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<UpdateEmployeeRequest, UpdateEmployeeRequestValidator, UpdateEmployeeCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<User, UsersRepository, UpdateEmployeeCommand>),
    order: 2
)]
[MediatRBehavior(
    typeof(EmployeeIdUniquenessPipelineBehavior<UpdateEmployeeRequest, UpdateEmployeeCommand>),
    order: 3
)]
public class UpdateEmployeeCommand : IRequest<IActionResult>, IRequestContainerCommand<UpdateEmployeeRequest>, IResourceAuthorizableCommand<User>
{
    public Guid Id { get; init; }
    public UpdateEmployeeRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public User Resource { get; set; }
}
