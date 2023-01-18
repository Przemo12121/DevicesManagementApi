using Database.Models;
using DevicesManagement.DataTransferObjects.Responses;
using MediatR;

namespace DevicesManagement.MediatR.Commands.Users;

public class DeleteEmployeeCommand : IRequest<UserDto>
{
    public Guid Id { get; init; }
}
