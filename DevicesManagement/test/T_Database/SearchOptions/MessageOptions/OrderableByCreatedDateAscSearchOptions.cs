namespace T_Database.SearchOptions.MessageOptions;

public class OrderableByCreatedDateAscSearchOptions : ISearchOptions<Message, DateTime>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Expression<Func<Message, DateTime>> Order { get; } = Message => Message.CreatedDate;

    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
