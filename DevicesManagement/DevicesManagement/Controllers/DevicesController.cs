using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;
using DevicesManagement.DataTransferObjects.Responses;

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
    public async Task<ActionResult<List<DeviceDto>>> ListAllDevices([FromQuery] PaginationRequest request)
    {
        var command = new ListAllDevicesCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpGet, Route("{employeeId}")]
    public async Task<ActionResult<List<DeviceDto>>> ListUserDevices([FromRoute] Guid employeeId, [FromBody] PaginationRequest request)
    {
        // TODO move to users controller?
        var command = new ListUserDevicesCommand() {  Id = employeeId, Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpGet, Route("{id}/commands")]
    public async Task<ActionResult<List<CommandDto>>> ListDeviceCommands([FromRoute] Guid id, [FromBody] PaginationRequest request)
    {
        var command = new ListDeviceCommandsCommand() { Request = request, ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpPost, Route("")]
    public async Task<ActionResult<DeviceDto>> RegisterDevice([FromBody] RegisterDeviceRequest request)
    {
        var command = new RegisterDeviceCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpPost, Route("{id}/commands")]
    public async Task<ActionResult<CommandDto>> RegisterCommand([FromRoute] Guid id, [FromBody] RegisterCommandRequest request)
    {
        var command = new RegisterCommandCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpDelete, Route("{id}")]
    public async Task<ActionResult<DeviceDto>> DeleteDevice([FromRoute] Guid id)
    {
        var command = new DeleteDeviceCommand() { ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }
}
