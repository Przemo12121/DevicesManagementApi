using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet, Route("/employees")]
    public List<IUser> GetEmployees(int? limit, int? offset, string? order)
    {
        return new List<IUser>();
    }

    [HttpPost, Route("/employees")]
    public IUser RegisterEmployee(IUser user)
    {
        // then send command to mocked device
        throw new NotImplementedException();
    }

    [HttpPatch, Route("/employees/:userId")]
    //!!! tutaj musze sie jeszcze pobawic w wysylanie i bindowanie requestow wiec beda parametry przerobione
    public IUser EditEmployee(int userId, IUser user)
    {
        // then send command to mocked device
        throw new NotImplementedException();
    }

    [HttpDelete, Route("/employees/:userId")]
    public string DeleteEmployee(int userId)
    {
        // then send command to mocked device
        throw new NotImplementedException();
    }
}
