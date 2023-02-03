using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet, Route("employees")]
    public async Task<IActionResult> GetEmployees([FromQuery] PaginationRequest request)
    {
        var command = new GetEmployeesQuery() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPost, Route("employees")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeRequest request)
    {
        var command = new RegisterEmployeeCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPatch, Route("employees/{id}")]
    public async Task<IActionResult> EditEmployee([FromRoute] Guid id, [FromBody] EditEmployeeRequest request)
    {
        var command = new EditEmployeeCommand() { Id = id, Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete, Route("employees/{id}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
    {
        var command = new DeleteEmployeeCommand() { ResourceId = id };
        var result = await _mediator.Send(command);
        return result;
    }


    [Authorize]
    [HttpPost, Route("")]
    public async Task<IActionResult> RegisterDevice([FromBody] RegisterDeviceRequest request)
    {
        var command = new RegisterDeviceCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [Authorize]
    [HttpGet, Route("{employeeId}/devices")]
    public async Task<IActionResult> ListUserDevices([FromRoute] Guid employeeId, [FromBody] PaginationRequest request)
    {
        var command = new GetUserDevicesQuery() { ResourceId = employeeId, Request = request };
        var result = await _mediator.Send(command);
        return result;
    }
}
