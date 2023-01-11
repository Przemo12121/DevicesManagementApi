﻿using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.DataTransferObjects.Responses;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
//[Authorize(Roles = "Admin")]
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
    public IUser RegisterEmployee([FromBody] CreateEmployeeRequest request)
    {
        // then send command to mocked device
        throw new NotImplementedException();
    }

    [HttpPatch, Route("employees/{id}")]
    public IUser EditEmployee([FromRoute] Guid id, [FromBody] EditEmployeeRequest request)
    {
        // then send command to mocked device
        throw new NotImplementedException();
    }

    [HttpDelete, Route("employees/{id}")]
    public string DeleteEmployee([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }
}
