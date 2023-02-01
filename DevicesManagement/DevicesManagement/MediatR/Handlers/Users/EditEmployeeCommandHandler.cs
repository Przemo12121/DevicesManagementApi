using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Users;

public class EditEmployeeCommandHandler : IRequestHandler<EditEmployeeCommand, UserDto>
{
    public Task<UserDto> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
