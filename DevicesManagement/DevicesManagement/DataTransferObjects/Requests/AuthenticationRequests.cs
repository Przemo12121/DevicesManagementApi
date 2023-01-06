using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace DevicesManagement.DataTransferObjects.Requests;

public record LoginWithCredentialsRequest : IRequest
{
    public string Login { get; init; }
    public string Password { get; init; }
};