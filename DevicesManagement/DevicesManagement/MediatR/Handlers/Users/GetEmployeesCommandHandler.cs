using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Users;

public class GetEmployeesCommandHandler : IRequestHandler<GetEmployeesQuery, List<UserDto>>
{
    public Task<List<UserDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
