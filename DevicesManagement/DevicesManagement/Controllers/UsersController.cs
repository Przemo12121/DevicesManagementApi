using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using DevicesManagement.DataTransferObjects;

namespace DevicesManagement.Controllers;

[Authentication.Jwt.JwtBearerFilter]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet, Route("employees")]
    public List<IUser> GetEmployees([FromQuery] PaginationRequest request)
    {
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
