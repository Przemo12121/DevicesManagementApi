namespace T_Database.SearchOptions.CommandHistoryOptions;

public class OrderableByCreatedDateAscSearchOptions : ISearchOptions<CommandHistory, DateTime>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Func<CommandHistory, DateTime> Order { get; } = CommandHistory => CommandHistory.CreatedDate;

    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
