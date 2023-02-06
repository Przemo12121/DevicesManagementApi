using DevicesManagement.MediatR.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace DevicesManagement.MediatR.PipelineBehaviors;

public class GetEmployeeAccessLevelPipelineBehavior<TRequest> : IPipelineBehavior<TRequest, IActionResult>
    where TRequest : IRequest<IActionResult>, IAccessLevelContainer
{

    private readonly IAccessLevelsRepository _accessLevelsRepository;

    public GetEmployeeAccessLevelPipelineBehavior(IAccessLevelsRepository accessLevelsRepository)
    {
        _accessLevelsRepository = accessLevelsRepository;
    }

    public async Task<IActionResult> Handle(TRequest request, RequestHandlerDelegate<IActionResult> next, CancellationToken cancellationToken)
    {
        request.AccessLevel = _accessLevelsRepository.FindByValue(AccessLevels.Employee) 
            ?? throw new Exception(StringMessages.InternalErrors.EMPLOYEE_ACCESS_LEVEL_NOT_FOUND);

        return await next();
    }
}
