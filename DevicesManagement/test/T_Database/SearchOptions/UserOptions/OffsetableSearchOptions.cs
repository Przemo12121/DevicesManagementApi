namespace T_Database.SearchOptions.UserOptions;

public class OffsetableSearchOptions : ISearchOptions<User, DateTime>
{
    public OffsetableSearchOptions(int offset) { Offset = offset; }

    public int Limit { get; } = 100;
    public int Offset { get; }
    public Func<User, DateTime> Order { get; } = device => device.CreatedDate;


    public OrderDirections OrderDirection { get; } = OrderDirections.DESCENDING;
}
