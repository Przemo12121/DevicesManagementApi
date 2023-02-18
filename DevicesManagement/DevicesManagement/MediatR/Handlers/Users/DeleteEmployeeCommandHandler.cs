using Database.Repositories.Interfaces;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;

    public DeleteEmployeeCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IActionResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        _usersRepository.Delete(request.Resource);
        await _usersRepository.SaveAsync();

        return new OkResult();
    }
}
