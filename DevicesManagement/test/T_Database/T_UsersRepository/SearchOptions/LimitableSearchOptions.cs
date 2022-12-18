using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace T_Database.T_UsersRepository.SearchOptions;

public class LimitableSearchOptions : ISearchOptions<User, DateTime>
{
    public LimitableSearchOptions(int limit) { Limit = limit; }

    public int Limit { get; }
    public int Offset { get; } = 0;
    public Func<User, DateTime> Order { get; } = user => user.CreatedDate;

    public OrderDirections OrderDirection { get; } = OrderDirections.ASCENDING;
}
