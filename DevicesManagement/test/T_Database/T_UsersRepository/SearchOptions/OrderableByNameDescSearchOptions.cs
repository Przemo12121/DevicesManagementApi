using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace T_Database.T_UsersRepository.SearchOptions;

public class OrderableByNameDescSearchOptions : ISearchOptions<User, string>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Func<User, string> Order { get; } = (user) => user.Name;

    public OrderDirections OrderDirection { get; } = OrderDirections.DESCENDING;
}
