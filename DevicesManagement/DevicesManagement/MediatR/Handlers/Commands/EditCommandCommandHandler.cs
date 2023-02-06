using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Commands;
using DevicesManagement.ModelsHandlers.ExtensionMethods;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Commands;

public class EditCommandCommandHandler : IRequestHandler<EditCommandCommand, IActionResult>
{
    private readonly ICommandsRepository _commandsRepository;
    public EditCommandCommandHandler(ICommandsRepository commandsRepository)
    {
        _commandsRepository = commandsRepository;
    } 

    public Task<IActionResult> Handle(EditCommandCommand request, CancellationToken cancellationToken)
    {
        request.Resource.UpdateWith(request.Request);

        _commandsRepository.Update(request.Resource);
        _commandsRepository.SaveChanges();

        var result = new OkObjectResult(request.Resource.Adapt<CommandDto>());
        return Task.FromResult<IActionResult>(result);
    }
}
