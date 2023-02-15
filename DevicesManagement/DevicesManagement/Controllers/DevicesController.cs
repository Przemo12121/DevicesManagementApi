using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;
using DevicesManagement.MediatR.Requests.Commands;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private readonly IMediator _mediator;
    public DevicesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet, Route("")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        var command = new GetAllDevicesQuery() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpGet, Route("{id}/commands")]
    public async Task<IActionResult> GetCommands([FromRoute] Guid id, [FromBody] PaginationRequest request)
    {
        var command = new GetCommandsQuery() { Request = request, ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpPatch, Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDeviceRequest request)
    {
        var command = new UpdateDeviceCommand() { Request = request, ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpPost, Route("{id}/commands")]
    public async Task<IActionResult> RegisterCommand([FromRoute] Guid id, [FromBody] RegisterCommandRequest request)
    {
        var command = new RegisterCommandCommand() { Request = request, ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpDelete, Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteDeviceCommand() { ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }
}
