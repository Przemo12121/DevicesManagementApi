﻿using Database.Models;
using Database.Repositories.ParallelRepositoryFactories;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IActionResult>
{
    private readonly ILocalAuthParallelRepositoriesFactory _parallelRepositoriesFactory;
    private readonly ISearchOptionsFactory<User, string> _searchOptionsFactory;

    public GetEmployeesQueryHandler(
        ILocalAuthParallelRepositoriesFactory parallelRepositoriesFactory, 
        ISearchOptionsFactory<User, string> searchOptionsFactory)
    {
        _parallelRepositoriesFactory = parallelRepositoriesFactory;
        _searchOptionsFactory = searchOptionsFactory;
    }

    public async Task<IActionResult> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.CreateFromRequest(request.Request);

        var employees = _parallelRepositoriesFactory.CreateUsersRepository()
            .FindEmployeesAsync(options);
        var totalCount = _parallelRepositoriesFactory.CreateUsersRepository()
            .CountEmployeesAsync();

        await Task.WhenAll(new Task[] { employees, totalCount });
        var result = new OkObjectResult(
            new PaginationResponseDto<UserDto>(
                totalCount.Result, 
                employees.Result.Adapt<List<UserDto>>()
            )
        );

        return result;
    }
}
