using Authentication;
using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, IActionResult>
{
    private readonly IIdentityProvider<User> _identityProvider;
    private readonly IUsersRepository _usersRepository;

    public RegisterEmployeeCommandHandler(IIdentityProvider<User> identityProvider, IUsersRepository usersRepository)
    {
        _identityProvider = identityProvider;
        _usersRepository = usersRepository;
    }

    public async Task<IActionResult> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
    {
        var newUser = _identityProvider.CreateIdentity(
            request.Request.EmployeeId, 
            request.Request.Name, 
            request.Request.Password, 
            request.AccessLevel
        );

        _usersRepository.Add(newUser);
        await _usersRepository.SaveAsync();

        return new OkObjectResult(newUser.Adapt<UserDto>());
    }
}
