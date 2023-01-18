using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Users;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, UserDto>
{
    public Task<UserDto> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
