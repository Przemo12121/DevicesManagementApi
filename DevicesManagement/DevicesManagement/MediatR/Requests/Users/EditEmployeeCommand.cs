using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Users;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<EditEmployeeRequest, EditEmployeeRequestValidator, EditEmployeeCommand>),
    order: 1
)]
public class EditEmployeeCommand : IRequest<IActionResult>, IRequestContainerCommand<EditEmployeeRequest>
{
    public Guid Id { get; init; }
    public EditEmployeeRequest Request { get; init; }
}
