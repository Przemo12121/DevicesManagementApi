namespace T_Database.SearchOptions.CommandOptions;

public class OrderableByNameDescSearchOptions : ISearchOptions<Command, string>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Func<Command, string> Order { get; } = Command => Command.Name;

    public OrderDirections OrderDirection { get; } = OrderDirections.DESCENDING;
}
