using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands.Authentication;

namespace DevicesManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost, Route("jwt/login")]
    [AllowAnonymous]
    public async Task<IActionResult> GrantJwt([FromBody] LoginWithCredentialsRequest request)
    {
        var command = new LoginWithCredentialsCommand() { Request = request };
        var result = await _mediator.Send(command);
        return result;
    }
}
