using Database.Models;
using Database.Models.Enums;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.Factories.SearchOptions;

public class UsersSearchOptionsFactory : ISearchOptionsFactory<User, string>
{
    public ISearchOptions<User, string> From(PaginationRequest request)
    {
        var orderSplitted = (request.Order?.ToLower() ?? "name:asc").Split(":");

        Func<User, string> order = orderSplitted.First() switch
        {
            "name" => (user) => user.Name,
            "eid" => (user) => user.EmployeeId,
            _ => throw new InvalidOperationException(StringMessages.InternalErrors.INVALID_ORDER_KEY)
        };

        return new CommonSearchOptions<User, string>
        {
            Limit = request.Limit ?? 12,
            Offset = request.Offset ?? 0,
            OrderDirection = orderSplitted.Last().Equals("asc") ? OrderDirections.ASCENDING : OrderDirections.DESCENDING,
            Order = order,
        };
    }
}
