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

    public Task<IActionResult> Handle(UpdateCommandCommand request, CancellationToken cancellationToken)
    {
        request.Resource.UpdateWith(request.Request);

        _commandsRepository.Update(request.Resource);
        _commandsRepository.SaveChanges();

        var result = new OkObjectResult(request.Resource.Adapt<CommandDto>());
        return Task.FromResult<IActionResult>(result);
    }
}
