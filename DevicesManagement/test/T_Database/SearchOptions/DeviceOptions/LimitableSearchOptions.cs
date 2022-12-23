using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace T_Database.SearchOptions.DeviceOptions;

public class LimitableSearchOptions : ISearchOptions<Device, DateTime>
{
    public LimitableSearchOptions(int limit) { Limit = limit; }

    public int Limit { get; }
    public int Offset { get; } = 0;
    public Func<Device, DateTime> Order { get; } = device => device.CreatedDate;

    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
