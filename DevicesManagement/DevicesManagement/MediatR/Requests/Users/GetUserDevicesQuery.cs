using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<User, GetUserDevicesQuery>),
    order: 1
)]
[MediatRBehavior(
    typeof(GetUserDevicesValidationPipelineBehavior),
    order: 2
)]
public class GetUserDevicesQuery 
    : IRequest<IActionResult>, IRequestContainerCommand<PaginationRequest>, IResourceAuthorizableCommand<User>
{
    public PaginationRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public User Resource { get; set; }
}
