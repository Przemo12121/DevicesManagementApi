using Database.Models.Base;
using Database.Models.Enums;
using System.Linq.Expressions;

namespace Database.Repositories.Interfaces;

/// <summary>
/// Object representing additional select constraints.
/// </summary>
/// <typeparam name="T">Type of entities.</typeparam>
public interface ISearchOptions<T, TOrderKey> where T : IDatabaseModel
{
    public int Limit { get; }

    public int Offset { get; }

    public Expression<Func<T, TOrderKey>> Order { get; }

    public OrderDirections OrderDirection { get; }
}
