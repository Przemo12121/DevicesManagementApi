using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Requests.Commands;
using DevicesManagement.ModelsHandlers.ExtensionMethods;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Commands;

public class UpdateCommandCommandHandler : IRequestHandler<UpdateCommandCommand, IActionResult>
{
    private readonly ICommandsRepository _commandsRepository;
    public UpdateCommandCommandHandler(ICommandsRepository commandsRepository)
    {
        _commandsRepository = commandsRepository;
    } 

    public async Task<IActionResult> Handle(UpdateCommandCommand request, CancellationToken cancellationToken)
    {
        request.Resource.UpdateWith(request.Request);

        _commandsRepository.Update(request.Resource);
        await _commandsRepository.SaveAsync();

        return new OkObjectResult(request.Resource.Adapt<CommandDto>());
    }
}
