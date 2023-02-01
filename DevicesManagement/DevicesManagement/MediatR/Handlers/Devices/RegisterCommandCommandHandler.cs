using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class RegisterCommandCommandHandler : IRequestHandler<RegisterCommandCommand, CommandDto>
{
    public Task<CommandDto> Handle(RegisterCommandCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
