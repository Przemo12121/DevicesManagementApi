using Database.Repositories.Interfaces;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;

    public DeleteCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IActionResult> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        _usersRepository.Delete(request.Resource);
        await _usersRepository.SaveAsync();

        return new OkResult();
    }
}
