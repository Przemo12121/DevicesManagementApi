using DevicesManagement.MediatR.Commands;
using Microsoft.AspNetCore.Mvc;

namespace T_DeviceManagement.T_MediatR.T_PipelineBehaviors.Dummy;

public record DummyValidableRequestCommand : IRequest<IActionResult>, IRequestContainerCommand<DummyRequest>
{
    public DummyRequest Request { get; init; }

}

public record DummyRequest();