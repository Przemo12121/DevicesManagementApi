using Database.Models;
using Database.Models.Enums;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;
using System.Linq.Expressions;

namespace DevicesManagement.ModelsHandlers.Factories.SearchOptions;

public class DevicesSearchOptionsFactory : ISearchOptionsFactory<Device, string>
{
    public ISearchOptions<Device, string> From(PaginationRequest request)
    {
        var orderSplitted = (request.Order?.ToLower() ?? "name:asc").Split(":");

        Expression<Func<Device, string>> order = orderSplitted.First() switch
        {
            "name" => (device) => device.Name,
            "eid" => (device) => device.EmployeeId,
            "address" => (device) => device.Address,
            _ => throw new InvalidOperationException(StringMessages.InternalErrors.INVALID_ORDER_KEY)
        };

        return new CommonSearchOptions<Device, string>
        {
            Limit = request.Limit ?? 24,
            Offset = request.Offset ?? 0,
            OrderDirection = orderSplitted.Last().Equals("asc") ? OrderDirections.ASCENDING : OrderDirections.DESCENDING,
            Order = order,
        };
    }
}