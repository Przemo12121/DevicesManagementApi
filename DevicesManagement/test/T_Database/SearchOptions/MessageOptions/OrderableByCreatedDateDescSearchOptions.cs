namespace T_Database.SearchOptions.MessageOptions;

public class OrderableByCreatedDateDescSearchOptions : ISearchOptions<Message, string>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Func<Message, string> Order { get; } = Message => Message.Content;

    public OrderDirections OrderDirection { get; } = OrderDirections.DESCENDING;
}
