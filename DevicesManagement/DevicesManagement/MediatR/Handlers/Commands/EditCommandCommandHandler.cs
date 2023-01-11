using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Commands;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Commands;

public class EditCommandCommandHandler : IRequestHandler<EditCommandCommand, CommandDto>
{
    public Task<CommandDto> Handle(EditCommandCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CommandDto());
    }
}
