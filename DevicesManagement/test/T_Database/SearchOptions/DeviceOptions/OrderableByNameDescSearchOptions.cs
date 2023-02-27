using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace T_Database.SearchOptions.DeviceOptions;

public class OrderableByNameDescSearchOptions : ISearchOptions<Device, string>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Expression<Func<Device, string>> Order { get; } = device => device.Name;

    public OrderDirections OrderDirection { get; } = OrderDirections.Descending;
}
