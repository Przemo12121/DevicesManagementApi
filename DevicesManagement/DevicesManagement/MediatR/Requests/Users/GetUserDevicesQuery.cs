using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<User, UsersRepository, GetUserDevicesQuery, List<DeviceDto>>),
    order: 1
)]
[MediatRBehavior(
    typeof(ListUserDevicesValidationPipelineBehavior),
    order: 2
)]
public class GetUserDevicesQuery : IRequest<List<DeviceDto>>, IRequestContainerCommand<PaginationRequest>, IResourceAuthorizableCommand<User>
{
    public PaginationRequest Request { get; init; }
    public Guid ResourceId { get; init; }
    public User Resource { get; set; }
}
