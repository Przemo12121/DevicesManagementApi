using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(GetEmployeesValidationPipelineBehavior), order: 1
)]
public class GetEmployeesQuery : IRequest<List<UserDto>>, IRequestContainerCommand<PaginationRequest>
{
    public PaginationRequest Request { get; init; }
}
