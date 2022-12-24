namespace T_Database.SearchOptions.UserOptions;

public class OrderableByNameAscSearchOptions : ISearchOptions<User, string>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Func<User, string> Order { get; } = device => device.Name;

    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
