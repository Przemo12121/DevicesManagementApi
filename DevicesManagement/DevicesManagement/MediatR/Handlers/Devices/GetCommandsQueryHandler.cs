using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class GetCommandsQueryHandler : IRequestHandler<GetCommandsQuery, IActionResult>
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly ISearchOptionsFactory<Command, string> _searchOptionsFactory;

    public GetCommandsQueryHandler(IDevicesRepository devicesRepository, ISearchOptionsFactory<Command, string> searchOptionsFactory)
    {
        _devicesRepository = devicesRepository;
        _searchOptionsFactory = searchOptionsFactory;
    }
    public Task<IActionResult> Handle(GetCommandsQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.From(request.Request);

        var commands = _devicesRepository.GetCommands(request.Resource.Id, options);

        var result = new OkObjectResult(commands.Adapt<List<CommandDto>>());
        return Task.FromResult<IActionResult>(result);
    }
}
