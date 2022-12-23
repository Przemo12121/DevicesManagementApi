using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace T_Database.SearchOptions.MessageOptions;

public class OffsetableSearchOptions : ISearchOptions<Message, DateTime>
{
    public OffsetableSearchOptions(int offset) { Offset = offset; }

    public int Limit { get; } = 100;
    public int Offset { get; }
    public Func<Message, DateTime> Order { get; } = Message => Message.CreatedDate;


    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
