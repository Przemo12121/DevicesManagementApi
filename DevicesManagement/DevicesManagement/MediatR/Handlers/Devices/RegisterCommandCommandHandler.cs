using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.ModelsHandlers.Factories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class RegisterCommandCommandHandler : IRequestHandler<RegisterCommandCommand, IActionResult>
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly ICommandsFactory<Command> _commandsFactory;

    public RegisterCommandCommandHandler(IDevicesRepository devicesRepository, ICommandsFactory<Command> commandsFactory)
    {
        _devicesRepository = devicesRepository;
        _commandsFactory = commandsFactory;
    }

    public async Task<IActionResult> Handle(RegisterCommandCommand request, CancellationToken cancellationToken)
    {
        var newCommand = _commandsFactory.From(request.Request);
        
        _devicesRepository.AddCommand(request.Resource, newCommand);
        await _devicesRepository.SaveAsync();

        return new OkObjectResult(newCommand.Adapt<CommandDto>());
    }
}
