using Database.Models;
using Database.Repositories;
using DevicesManagement.MediatR.Commands.Commands;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Commands;

public class EditCommandCommandHandler : IRequestHandler<EditCommandCommand, Command>
{
    private readonly CommandsRepository _commandsRepository;
    public EditCommandCommandHandler(CommandsRepository commandsRepository)
    {
        _commandsRepository = commandsRepository;
    } 

    public Task<Command> Handle(EditCommandCommand request, CancellationToken cancellationToken)
    {
        if (request.Request.Description is not null)
            request.Resource.Description = request.Request.Description;
        if (request.Request.Name is not null)
            request.Resource.Name = request.Request.Name;
        if (request.Request.Body is not null)
            request.Resource.Body = request.Request.Body;

        _commandsRepository.Update(request.Resource);

        return Task.FromResult(request.Resource);
    }
}
