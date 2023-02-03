using DevicesManagement.MediatR.Commands.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.Handlers.Commands;

public class RunCommandCommandandler : IRequestHandler<RunCommandCommand, IActionResult>
{
    public Task<IActionResult> Handle(RunCommandCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
