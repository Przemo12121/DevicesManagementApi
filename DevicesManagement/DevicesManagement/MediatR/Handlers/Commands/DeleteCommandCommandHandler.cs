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

    public async Task<IActionResult> Handle(DeleteCommandCommand request, CancellationToken cancellationToken)
    {
        _commandsRepository.Delete(request.Resource!);
        await _commandsRepository.SaveAsync();

        return new OkResult();
    }
}
