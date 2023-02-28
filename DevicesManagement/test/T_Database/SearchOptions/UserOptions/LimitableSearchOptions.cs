namespace T_Database.SearchOptions.UserOptions;

public class LimitableSearchOptions : ISearchOptions<User, DateTime>
{
    public LimitableSearchOptions(int limit) { Limit = limit; }

    public int Limit { get; }
    public int Offset { get; } = 0;
    public Expression<Func<User, DateTime>> Order { get; } = device => device.CreatedDate;

    public OrderDirections OrderDirection { get; } = OrderDirections.Ascending;
}
