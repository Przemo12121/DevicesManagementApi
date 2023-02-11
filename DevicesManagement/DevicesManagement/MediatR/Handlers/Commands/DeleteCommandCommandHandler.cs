using Database.Repositories.Interfaces;
using DevicesManagement.MediatR.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class DeleteCommandCommandHandler : IRequestHandler<DeleteCommandCommand, IActionResult>
{
    private readonly ICommandsRepository _commandsRepository;

    public DeleteCommandCommandHandler(ICommandsRepository commandsRepository)
    {
        _commandsRepository = commandsRepository;
    }

    public Task<IActionResult> Handle(DeleteCommandCommand request, CancellationToken cancellationToken)
    {
        _commandsRepository.Delete(request.Resource!);
        _commandsRepository.SaveChanges();

        return Task.FromResult<IActionResult>(new OkResult());
    }
}
