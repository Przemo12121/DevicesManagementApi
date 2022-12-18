using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace T_Database.T_UsersRepository.SearchOptions;

public class OffsetableSearchOptions : ISearchOptions<User, DateTime>
{
    public OffsetableSearchOptions(int offset) { Offset = offset; }

    public int Limit { get; } = 100;
    public int Offset { get; }
    public Func<User, DateTime> Order { get; } = user => user.CreatedDate;


    public OrderDirections OrderDirection { get; } = OrderDirections.DESCENDING;
}
