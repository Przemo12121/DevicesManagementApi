using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Users;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterEmployeeRequest, RegisterEmployeeRequestValidator, RegisterEmployeeCommand>),
    order: 1
)]
public class RegisterEmployeeCommand : IRequest<IActionResult>, IRequestContainerCommand<RegisterEmployeeRequest>
{
    public RegisterEmployeeRequest Request { get; init; }
}
