using Database.Models;
using Database.Models.Enums;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;
using System.Linq.Expressions;

namespace DevicesManagement.ModelsHandlers.Factories.SearchOptions;

public class CommandsSearchOptionsFactory : ISearchOptionsFactory<Command, string>
{
    public ISearchOptions<Command, string> CreateFromRequest(PaginationRequest request)
    {
        var orderSplitted = (request.Order?.ToLower() ?? "name:asc").Split(":");

        Expression<Func<Command, string>> order = orderSplitted.First() switch
        {
            "body" => command => command.Body,
            "name" => command => command.Name,
            _ => throw new ArgumentOutOfRangeException(StringMessages.InternalErrors.INVALID_ORDER_KEY)
        };

        return new CommonSearchOptions<Command, string>
        {
            Limit = request.Limit ?? 48,
            Offset = request.Offset ?? 0,
            OrderDirection = orderSplitted.Last().Equals("asc") ? OrderDirections.Ascending : OrderDirections.Descending,
            Order = order,
        };
    }
}
