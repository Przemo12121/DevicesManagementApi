using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.MediatR.Commands;

namespace T_DeviceManagement.T_MediatR.T_PipelineBehaviors.Dummy;

internal class DummyEmployeeIdContainerRequest : IRequest<IActionResult>, IRequestContainerCommand<DummyEmployeeIdRequest>
{
    public DummyEmployeeIdRequest Request { get; init; }
}

internal class DummyEmployeeIdRequest : IEmployeeIdContainer
{
    public string? EmployeeId { get; init; }
}