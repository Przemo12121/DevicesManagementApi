namespace T_Database.SearchOptions.CommandHistoryOptions;

public class LimitableSearchOptions : ISearchOptions<CommandHistory, DateTime>
{
    public LimitableSearchOptions(int limit) { Limit = limit; }

    public int Limit { get; }
    public int Offset { get; } = 0;
    public Expression<Func<CommandHistory, DateTime>> Order { get; } = Command => Command.CreatedDate;

    public OrderDirections OrderDirection { get; } = OrderDirections.Ascending;
}
