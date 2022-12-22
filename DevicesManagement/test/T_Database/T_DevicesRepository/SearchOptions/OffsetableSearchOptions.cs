using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace T_Database.T_DevicesRepository.SearchOptions;

public class OffsetableSearchOptions : ISearchOptions<Device, DateTime>
{
    public OffsetableSearchOptions(int offset) { Offset = offset; }

    public int Limit { get; } = 100;
    public int Offset { get; }
    public Func<Device, DateTime> Order { get; } = device => device.CreatedDate;


    public OrderDirections OrderDirection { get; } = OrderDirections.DESCENDING;
}
