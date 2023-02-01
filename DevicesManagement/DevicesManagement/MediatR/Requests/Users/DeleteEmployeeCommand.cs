using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(ResourceAuthorizationPipelineBehavior<User, UsersRepository, DeleteEmployeeCommand, UserDto>),
    order: 1
)]
public class DeleteEmployeeCommand : IRequest<UserDto>, IResourceAuthorizableCommand<User>
{
    public Guid ResourceId { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
    public User Resource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
