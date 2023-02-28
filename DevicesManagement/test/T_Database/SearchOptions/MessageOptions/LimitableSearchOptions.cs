namespace T_Database.SearchOptions.MessageOptions;

public class LimitableSearchOptions : ISearchOptions<Message, DateTime>
{
    public LimitableSearchOptions(int limit) { Limit = limit; }

    public int Limit { get; }
    public int Offset { get; } = 0;
    public Expression<Func<Message, DateTime>> Order { get; } = Command => Command.CreatedDate;

    public OrderDirections OrderDirection { get; } = OrderDirections.Ascending;
}
