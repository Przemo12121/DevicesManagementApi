using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.PipelineBehaviors.Paginations;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;

namespace DevicesManagement.MediatR.Commands.Devices;

[MediatRBehavior(
    typeof(ListUserDevicesValidationPipelineBehavior),
    order: 1
)]
public class ListUserDevicesCommand : IRequest<List<DeviceDto>>, IRequestContainerCommand<PaginationRequest>
{
    public Guid Id { get; init; }
    public PaginationRequest Request { get; init; }
}
