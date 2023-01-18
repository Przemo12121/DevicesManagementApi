using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.DataTransferObjects.Responses;

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
    public async Task<ActionResult<List<UserDto>>> GetEmployees([FromQuery] PaginationRequest request)
    {
        var command = new GetEmployeesCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPost, Route("employees")]
    public async Task<ActionResult<UserDto>> RegisterEmployee([FromBody] RegisterEmployeeRequest request)
    {
        var command = new RegisterEmployeeCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPatch, Route("employees/{id}")]
    public async Task<ActionResult<UserDto>> EditEmployee([FromRoute] Guid id, [FromBody] EditEmployeeRequest request)
    {
        var command = new EditEmployeeCommand() { Id = id, Request = request };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete, Route("employees/{id}")]
    public async Task<ActionResult<UserDto>> DeleteEmployee([FromRoute] Guid id)
    {
        var command = new DeleteEmployeeCommand() { Id = id };
        var result = await _mediator.Send(command);
        return result;
    }
}
