using DevicesManagement.MediatR.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Commands;

public class RunCommandCommandHandler : IRequestHandler<RunCommandCommand, IActionResult>
{
    public Task<IActionResult> Handle(RunCommandCommand request, CancellationToken cancellationToken)
    {
        // TODO impl faking devices
        var result = new OkObjectResult(StringMessages.Successes.COMMAND_RUN);
        return Task.FromResult<IActionResult>(result);
    }
}
