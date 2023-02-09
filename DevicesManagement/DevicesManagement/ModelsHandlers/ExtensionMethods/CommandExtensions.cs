using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.ExtensionMethods;

public static class CommandExtensions
{
    public static void UpdateWith(this Command command, UpdateCommandRequest request)
    {
        if (request.Description is not null)
            command.Description = request.Description;
        if (request.Name is not null)
            command.Name = request.Name;
        if (request.Body is not null)
            command.Body = request.Body;

        command.UpdatedDate = DateTime.UtcNow;
    }
}
