using Authentication;
using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.ModelsHandlers.ExtensionMethods;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IIdentityProvider<User> _identityProvider;

    public UpdateEmployeeCommandHandler(IUsersRepository usersRepository, IIdentityProvider<User> identityProvider)
    {
        _usersRepository = usersRepository;
        _identityProvider = identityProvider;
    }

    public async Task<IActionResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        request.Resource.UpdateWith(request.Request, _identityProvider);

        _usersRepository.Update(request.Resource);
        await _usersRepository.SaveAsync();

        return new OkObjectResult(request.Resource.Adapt<UserDto>());
    }
}
