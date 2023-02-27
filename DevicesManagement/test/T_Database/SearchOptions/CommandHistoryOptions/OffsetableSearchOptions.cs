namespace T_Database.SearchOptions.CommandHistoryOptions;

public class OffsetableSearchOptions : ISearchOptions<CommandHistory, DateTime>
{
    public OffsetableSearchOptions(int offset) { Offset = offset; }

    public int Limit { get; } = 100;
    public int Offset { get; }
    public Expression<Func<CommandHistory, DateTime>> Order { get; } = CommandHistory => CommandHistory.CreatedDate;


    public OrderDirections OrderDirection { get; } = OrderDirections.Ascending;
}
