using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class GetEmployeesCommandHandler : IRequestHandler<GetEmployeesQuery, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ISearchOptionsFactory<User, string> _searchOptionsFactory;

    public GetEmployeesCommandHandler(IUsersRepository usersRepository, ISearchOptionsFactory<User, string> searchOptionsFactory)
    {
        _usersRepository = usersRepository;
        _searchOptionsFactory = searchOptionsFactory;
    }

    public Task<IActionResult> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.From(request.Request);

        var employees = _usersRepository.FindEmployees(options);

        var result = new OkObjectResult(employees.Adapt<List<UserDto>>());
        return Task.FromResult<IActionResult>(result);
    }
}
