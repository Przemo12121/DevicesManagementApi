using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.Handlers.Commands;

public class RunCommandCommandandler : IRequestHandler<RunCommandCommand, string>
{
    public Task<string> Handle(RunCommandCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult("");
    }
}
