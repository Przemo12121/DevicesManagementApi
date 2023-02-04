using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.Factories;

public class CommandsFactory : ICommandsFactory<Command>
{
    public Command From(RegisterCommandRequest request)
        => new()
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            Body = request.Body,
            Name = request.Name,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            CommandHistories = new()
        };
}
