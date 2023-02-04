using Database.Repositories;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, IActionResult>
{
    private readonly UsersRepository _usersRepository;

    public DeleteEmployeeCommandHandler(UsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<IActionResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        _usersRepository.Delete(request.Resource);
        _usersRepository.SaveChanges();

        return Task.FromResult<IActionResult>(new OkResult());
    }
}
