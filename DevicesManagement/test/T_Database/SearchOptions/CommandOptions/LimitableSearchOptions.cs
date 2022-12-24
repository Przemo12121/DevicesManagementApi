namespace T_Database.SearchOptions.CommandOptions;

public class LimitableSearchOptions : ISearchOptions<Command, DateTime>
{
    public LimitableSearchOptions(int limit) { Limit = limit; }

    public int Limit { get; }
    public int Offset { get; } = 0;
    public Func<Command, DateTime> Order { get; } = Command => Command.CreatedDate;

    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
