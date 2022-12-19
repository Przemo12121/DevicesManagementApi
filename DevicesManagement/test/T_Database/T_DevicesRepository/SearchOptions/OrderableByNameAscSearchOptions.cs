using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace T_Database.T_DevicesRepository.SearchOptions;

public class OrderableByNameAscSearchOptions : ISearchOptions<Device, string>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Func<Device, string> Order { get; } = device => device.Name;

    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
