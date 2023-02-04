using Database.Models;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<User, DeleteEmployeeCommand>),
    order: 1
)]
public class DeleteEmployeeCommand 
    : IRequest<IActionResult>, IResourceAuthorizableCommand<User>
{
    public Guid ResourceId { get; init; }
    public User Resource { get; set; }
}
