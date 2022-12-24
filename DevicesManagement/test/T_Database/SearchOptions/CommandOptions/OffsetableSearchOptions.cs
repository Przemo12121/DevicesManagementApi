namespace T_Database.SearchOptions.CommandOptions;

public class OffsetableSearchOptions : ISearchOptions<Command, DateTime>
{
    public OffsetableSearchOptions(int offset) { Offset = offset; }

    public int Limit { get; } = 100;
    public int Offset { get; }
    public Func<Command, DateTime> Order { get; } = Command => Command.CreatedDate;


    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
