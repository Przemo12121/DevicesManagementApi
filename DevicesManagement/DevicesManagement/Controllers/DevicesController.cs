using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using DevicesManagement.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class DevicesController : ControllerBase
{
    [HttpGet, Route("")]
    public List<IUser> ListAllDevices([FromQuery] PaginationRequest request)
    {
        // endpoint will be used to list brief information about all devices
        return new List<IUser>();
    }

    [HttpGet, Route("{employeeId}")]
    public List<IUser> ListUserDevices([FromRoute] Guid employeeId)
    {
        // endpoint will be used to list brief information about requesting user's devices
        return new List<IUser>();
    }

    [HttpGet, Route("{id}/commands")]
    public List<ICommand> ListDeviceCommands([FromRoute] Guid id, [FromBody] PaginationRequest request)
    {
        // return commands registered to device
        return new List<ICommand>();
    }

    [HttpPost, Route("")]
    public IDevice RegisterDevice([FromBody] CreateDeviceRequest request)
    {
        // adds new device
        throw new NotImplementedException();
    }

    [HttpPost, Route("{id}/commands")]
    public ICommand RegisterCommand([FromRoute] Guid id, [FromBody] CreateCommandRequest request)
    {
        // then send create new command
        throw new NotImplementedException();
    }

    [HttpDelete, Route("{id}")]
    public string DeleteDevice([FromRoute] Guid id)
    {
        // delete device
        return "Not yet implemented";
    }
}
