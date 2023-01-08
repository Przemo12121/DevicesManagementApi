using DevicesManagement.DataTransferObjects.Requests;
using MediatR;

namespace DevicesManagement.MediatR.Commands.Authentication;

public record LoginWithCredentialsCommand : IRequest
{
    public string Login { get; init; }
    public string Password { get; init; }

    public static LoginWithCredentialsCommand FromRequest(LoginWithCredentialsRequest request)
    {
        return new LoginWithCredentialsCommand
        {
            Login = request.Login,
            Password = request.Password,
        };
    }
};