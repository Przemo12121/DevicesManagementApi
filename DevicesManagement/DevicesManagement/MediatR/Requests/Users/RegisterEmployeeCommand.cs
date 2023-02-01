using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.Validations.Users;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<RegisterEmployeeRequest, RegisterEmployeeRequestValidator, RegisterEmployeeCommand, UserDto>),
    order: 1
)]
public class RegisterEmployeeCommand : IRequest<UserDto>, IRequestContainerCommand<RegisterEmployeeRequest>
{
    public RegisterEmployeeRequest Request { get; init; }
}
