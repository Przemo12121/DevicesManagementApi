using DevicesManagement.MediatR.Commands;

namespace T_DeviceManagement.T_MediatR.T_PipelineBehaviors.Dummy;

public record DummyValidableRequestCommand : IRequest<string>, IRequestContainerCommand<DummyRequest>
{
    public DummyRequest Request { get; init; }

}

public record DummyRequest();