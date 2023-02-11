using Database.Models;
using Database.Models.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.MediatR.Requests;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterEmployeeRequest, RegisterEmployeeCommand>),
    order: 1
)]
[MediatRBehavior(
    typeof(GetEmployeeAccessLevelPipelineBehavior<RegisterEmployeeCommand>),
    order: 2
)]
[MediatRBehavior(
    typeof(EmployeeIdUniquenessPipelineBehavior<RegisterEmployeeRequest, RegisterEmployeeCommand>),
    order: 3
)]
public class RegisterEmployeeCommand 
    : IRequest<IActionResult>, IRequestContainerCommand<RegisterEmployeeRequest>, IAccessLevelContainer
{
    public RegisterEmployeeRequest Request { get; init; }

    public AccessLevel AccessLevel { get; set; }
}
