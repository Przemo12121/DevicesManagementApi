using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ListAllDevicesValidationPipelineBehavior),
    order: 1
)]
public class GetAllDevicesQuery : IRequest<IActionResult>, IRequestContainerCommand<PaginationRequest>
{
    public PaginationRequest Request { get; init; }
}
