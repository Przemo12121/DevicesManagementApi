using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using DevicesManagement.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CommandsController : ControllerBase
{
    [HttpPost, Route("{id}/run")]
    public string RunCommand([FromRoute] Guid id)
    {
        return "Not yet implemented";
    }

    [HttpPatch, Route("{id}")]
    public ICommand EditCommand([FromRoute] Guid id, [FromBody] EditCommandRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete, Route("{id}")]
    public string DeleteCommand([FromRoute] Guid id)
    {
        return "Not yet implemented";
    }
}
