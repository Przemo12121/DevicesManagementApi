﻿namespace T_Database.SearchOptions.UserOptions;

public class OrderableByNameDescSearchOptions : ISearchOptions<User, string>
{

    public int Limit { get; } = 100;
    public int Offset { get; } = 0;
    public Expression<Func<User, string>> Order { get; } = device => device.Name;

    public OrderDirections OrderDirection { get; } = OrderDirections.DESCENDING;
}
