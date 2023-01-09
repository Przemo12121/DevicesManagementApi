using Microsoft.AspNetCore.Mvc;
using Database.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR;
using DevicesManagement.MediatR.Commands.Commands;
using DevicesManagement.DataTransferObjects.Responses;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CommandsController(IMediator mediator) 
    {
        _mediator = mediator;
    }

    [HttpPost, Route("{id}/run")]
    public async Task<ActionResult<string>> RunCommand([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new RunCommandCommand() { ResourceId = id });
        //throw new NotImplementedException();
        if (result == null)
        {
            return new EmptyResult();
        }
        return result;
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
