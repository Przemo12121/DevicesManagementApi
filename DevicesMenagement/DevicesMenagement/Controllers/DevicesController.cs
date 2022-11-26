using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        [HttpGet, Route("/")]
        public List<IUser> ListAllDevices()
        {
            // endpoint will be used to list brief information about all devices
            return new List<IUser>();
        }

        [HttpGet, Route("/:employeeId")]
        public List<IUser> ListUserDevices(int employeeId)
        {
            // endpoint will be used to list brief information about requesting user's devices
            return new List<IUser>();
        }

        [HttpGet, Route("/:deviceId/commands")]
        public List<ICommand> ListDeviceCommands(int deviceId)
        {
            // return commands registered to device
            return new List<ICommand>();
        }

        [HttpPost, Route("/")]
        public IUser RegisterDevice(IUser device)
        {
            // adds new device
            throw new NotImplementedException();
        }

        [HttpPost, Route("/:deviceId/commands")]
        public ICommand RegisterCommand(int deviceId, ICommand command)
        {
            // then send create new command
            throw new NotImplementedException();
        }

        [HttpDelete, Route("/:deviceId")]
        public string DeleteDevice(int deviceId)
        {
            // delete device
            return "Not yet implemented";
        }
    }
}
