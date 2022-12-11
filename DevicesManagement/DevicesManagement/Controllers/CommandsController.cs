using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;

namespace DevicesMenagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    [HttpPost, Route("/:commandId/run")]
    public string RunCommand(int commandId)
    {
        // then send command to mocked device
        return "Not yet implemented";
    }

    [HttpPatch, Route("/:commandId")]
    //!!! tutaj musze sie jeszcze pobawic w wysylanie i bindowanie requestow wiec beda parametry przerobione
    public ICommand EditCommand(int commandId, ICommand command)
    {
        // then send command to mocked device
        throw new NotImplementedException(); 
    }

    [HttpDelete, Route("/:commandId")]
    public string DeleteCommand(int commandId)
    {
        // then send command to mocked device
        return "Not yet implemented";
    }
}
