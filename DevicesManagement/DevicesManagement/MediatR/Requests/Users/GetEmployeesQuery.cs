using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Users;

[MediatRBehavior(
    typeof(GetEmployeesValidationPipelineBehavior), order: 1
)]
public class GetEmployeesQuery 
    : IRequest<IActionResult>, IRequestContainerCommand<PaginationRequest>
{
    public PaginationRequest Request { get; init; }
}
