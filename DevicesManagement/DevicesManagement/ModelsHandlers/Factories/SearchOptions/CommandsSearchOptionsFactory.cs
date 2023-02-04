using Database.Models;
using Database.Models.Enums;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.Factories.SearchOptions;

public class CommandsSearchOptionsFactory : ISearchOptionsFactory<Command, string>
{
    public ISearchOptions<Command, string> From(PaginationRequest request)
    {
        var orderSplitted = (request.Order?.ToLower() ?? "name:asc").Split(":");

        Func<Command, string> order = orderSplitted.First() switch
        {
            "name" => (command) => command.Name,
            "body" => (command) => command.Body,
            _ => throw new InvalidOperationException(StringMessages.InternalErrors.INVALID_ORDER_KEY)
        };

        return new CommonSearchOptions<Command, string>
        {
            Limit = request.Limit ?? 48,
            Offset = request.Offset ?? 0,
            OrderDirection = orderSplitted.Last().Equals("asc") ? OrderDirections.ASCENDING : OrderDirections.DESCENDING,
            Order = order,
        };
    }
}
