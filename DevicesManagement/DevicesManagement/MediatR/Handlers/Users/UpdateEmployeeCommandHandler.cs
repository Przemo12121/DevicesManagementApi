using Authentication;
using Database.Models;
using Database.Repositories;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.ModelsHandlers.ExtensionMethods;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IActionResult>
{
    private readonly UsersRepository _usersRepository;
    private readonly IIdentityProvider<User> _identityProvider;

    public UpdateEmployeeCommandHandler(UsersRepository usersRepository, IIdentityProvider<User> identityProvider)
    {
        _usersRepository = usersRepository;
        _identityProvider = identityProvider;
    }

    public Task<IActionResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        request.Resource.UpdateWith(request.Request, _identityProvider);

        _usersRepository.Update(request.Resource);
        _usersRepository.SaveChanges();

        var result = new OkObjectResult(request.Resource.Adapt<UserDto>());
        return Task.FromResult<IActionResult>(result);
    }
}
