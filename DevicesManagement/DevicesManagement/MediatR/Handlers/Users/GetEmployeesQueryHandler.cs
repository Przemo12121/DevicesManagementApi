using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ISearchOptionsFactory<User, string> _searchOptionsFactory;

    public GetEmployeesQueryHandler(IUsersRepository usersRepository, ISearchOptionsFactory<User, string> searchOptionsFactory)
    {
        _usersRepository = usersRepository;
        _searchOptionsFactory = searchOptionsFactory;
    }

    public Task<IActionResult> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.From(request.Request);

        var employees = _usersRepository.FindEmployees(options);
        var totalCount = _usersRepository.CountEmployees();

        var result = new OkObjectResult(
            new PaginationResponseDto<UserDto>(totalCount, employees.Adapt<List<UserDto>>())
        );
        return Task.FromResult<IActionResult>(result);
    }
}
