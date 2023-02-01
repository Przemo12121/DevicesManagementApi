using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Users;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<EditEmployeeRequest, EditEmployeeRequestValidator, EditEmployeeCommand, UserDto>),
    order: 1
)]
public class EditEmployeeCommand : IRequest<UserDto>, IRequestContainerCommand<EditEmployeeRequest>
{
    public Guid Id { get; init; }
    public EditEmployeeRequest Request { get; init; }
}
