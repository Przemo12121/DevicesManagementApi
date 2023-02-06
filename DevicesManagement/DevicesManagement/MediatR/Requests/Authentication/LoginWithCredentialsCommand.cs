using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Authentication;

/*[MediatRBehavior(
    typeof(RequestValidationPipelineBehavior<EditCommandRequest, EditCommandCommand>),
    order: 1
)]*/
[MediatRBehavior(
    typeof(CredentialsVerificationPipelineBehavior),
    order: 2
)]
public class LoginWithCredentialsCommand : IRequest<IActionResult>, IRequestContainerCommand<LoginWithCredentialsRequest>
{
    public LoginWithCredentialsRequest Request { get; init; }
    public User Identity { get; set; }
};