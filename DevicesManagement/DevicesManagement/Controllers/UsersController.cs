using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using DevicesManagement.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Authentication.Jwt;
using Database.Models;
using Microsoft.IdentityModel.Tokens;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet, Route("employees")]
    public List<IUser> GetEmployees([FromQuery] PaginationRequest request)
    {
        //var x = User; // user is retrieved with authorize, from jwt bearer authenticator

        return new List<IUser>();
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
