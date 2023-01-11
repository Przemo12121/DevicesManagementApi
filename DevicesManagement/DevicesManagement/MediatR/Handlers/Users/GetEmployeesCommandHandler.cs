using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Users;

public class GetEmployeesCommandHandler : IRequestHandler<GetEmployeesCommand, List<UserDto>>
{
    public Task<List<UserDto>> Handle(GetEmployeesCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("lalla");
        throw new NotImplementedException();
    }
}
