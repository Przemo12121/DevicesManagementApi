using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet, Route("/employees")]
        public List<IUser> GetEmployees()
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
        public IUser EditEmployee(int userId)
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
}
