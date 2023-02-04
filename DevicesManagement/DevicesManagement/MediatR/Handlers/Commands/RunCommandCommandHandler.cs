using DevicesManagement.MediatR.Commands.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.Handlers.Commands;

public class RunCommandCommandandler : IRequestHandler<RunCommandCommand, IActionResult>
{
    public Task<IActionResult> Handle(RunCommandCommand request, CancellationToken cancellationToken)
    {
        // TODO impl faking devices
        var result = new OkObjectResult(StringMessages.Successes.COMMAND_RUN);
        return Task.FromResult<IActionResult>(result);
    }
}
