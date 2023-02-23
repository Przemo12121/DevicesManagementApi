using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR;
using DevicesManagement.MediatR.Commands.Users;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet, Route("employees")]
    public async Task<IActionResult> GetEmployees([FromQuery] PaginationRequest request)
    {
        var command = new GetEmployeesQuery() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, Route("employees")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeRequest request)
    {
        var command = new RegisterEmployeeCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch, Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest request)
    {
        var command = new UpdateCommand() { ResourceId = id, Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete, Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteCommand() { ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }


    [Authorize(Roles = "Admin")]
    [HttpPost, Route("{id}/devices")]
    public async Task<IActionResult> RegisterDevice([FromRoute] Guid id, [FromBody] RegisterDeviceRequest request)
    {
        var command = new RegisterDeviceCommand() { Request = request, ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpGet, Route("{id}/devices")]
    public async Task<IActionResult> GetDevices([FromRoute] Guid id, [FromQuery] PaginationRequest request)
    {
        var command = new GetUserDevicesQuery() { ResourceId = id, Request = request };
        var result = await _mediator.Send(command);
        return result;
    }
}
