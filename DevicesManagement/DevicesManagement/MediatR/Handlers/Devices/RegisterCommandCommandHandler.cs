using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.ModelsHandlers;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class RegisterCommandCommandHandler : IRequestHandler<RegisterCommandCommand, IActionResult>
{
    private readonly DevicesRepository _devicesRepository;
    private readonly ICommandsFactory<Command> _commandsFactory;
    public RegisterCommandCommandHandler(DevicesRepository devicesRepository, ICommandsFactory<Command> commandsFactory)
    {
        _devicesRepository = devicesRepository;
        _commandsFactory = commandsFactory;
    }

    public Task<IActionResult> Handle(RegisterCommandCommand request, CancellationToken cancellationToken)
    {
        var newCommand = _commandsFactory.From(request.Request);
        
        _devicesRepository.AddCommand(request.Resource, newCommand);
        _devicesRepository.SaveChanges();

        var result = new OkObjectResult(newCommand.Adapt<CommandDto>());
        return Task.FromResult((IActionResult)result);
    }
}
