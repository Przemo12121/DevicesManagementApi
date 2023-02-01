using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Users;

public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, UserDto>
{
    public Task<UserDto> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
