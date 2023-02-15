using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR;
using DevicesManagement.MediatR.Requests.Commands;

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
    public async Task<IActionResult> Run([FromRoute] Guid id)
    {
        var command = new RunCommandCommand() { ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPatch, Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCommandRequest request)
    {
        var command = new UpdateCommandCommand() { ResourceId = id, Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete, Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteCommandCommand() { ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }
}
