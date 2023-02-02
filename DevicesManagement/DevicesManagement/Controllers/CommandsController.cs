using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR;
using DevicesManagement.MediatR.Commands.Commands;
using DevicesManagement.DataTransferObjects.Responses;
using Mapster;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommandsController(IMediator mediator) 
    {
        _mediator = mediator;
    }

    [HttpPost, Route("{id}/run")]
    public async Task<ActionResult<string>> RunCommand([FromRoute] Guid id)
    {
        var command = new RunCommandCommand() { ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPatch, Route("{id}")]
    public async Task<CommandDto> EditCommand([FromRoute] Guid id, [FromBody] EditCommandRequest request)
    {
        var command = new EditCommandCommand() { ResourceId = id, Request = request };
        var result = await _mediator.Send(command);
        return result.Adapt<CommandDto>();
    }

    [HttpDelete, Route("{id}")]
    public async Task<ActionResult<string>> DeleteCommand([FromRoute] Guid id)
    {
        var command = new RunCommandCommand() { ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }
}
