using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevicesManagement.DataTransferObjects.Requests;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.HttpSys;
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
    public async void GrantJwt([FromBody] LoginWithCredentialsRequest request)
    {
        var command = LoginWithCredentialsCommand.FromRequest(request);
        await _mediator.Send(command);
    }
}
