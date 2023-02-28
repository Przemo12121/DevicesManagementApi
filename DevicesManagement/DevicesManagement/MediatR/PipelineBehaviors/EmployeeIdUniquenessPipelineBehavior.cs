using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.Errors;
using DevicesManagement.MediatR.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.PipelineBehaviors;

public class EmployeeIdUniquenessPipelineBehavior<T, TRequest> : IPipelineBehavior<TRequest, IActionResult>
    where T : IEmployeeIdContainer
    where TRequest : IRequest<IActionResult>, IRequestContainerCommand<T>
{
    private readonly IUsersRepository _usersRepository;

    public EmployeeIdUniquenessPipelineBehavior(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IActionResult> Handle(TRequest request, RequestHandlerDelegate<IActionResult> next, CancellationToken cancellationToken)
    {
        var existingEmployee = request.Request.EmployeeId is not null
            ? await _usersRepository.FindByEmployeeIdAsync(request.Request.EmployeeId)
            : null;

        if (existingEmployee is not null)
        {
            return ErrorResponses.CreateDetailed(
                StatusCodes.Status409Conflict,
                StringMessages.HttpErrors.Details.EmployeeIdTaken
            );
        }

        return await next();
    }
}
