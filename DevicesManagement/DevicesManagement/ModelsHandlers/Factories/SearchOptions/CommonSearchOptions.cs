using Database.Models.Base;
using Database.Models.Enums;
using Database.Repositories.Interfaces;

namespace DevicesManagement.ModelsHandlers.Factories.SearchOptions;

public class CommonSearchOptions<T, U> : ISearchOptions<T, U>
    where T : IDatabaseModel
{
    public int Limit { get; init; }

    public int Offset { get; init; }

    public Func<T, U> Order { get; init; }

    public OrderDirections OrderDirection { get; init; }
}
