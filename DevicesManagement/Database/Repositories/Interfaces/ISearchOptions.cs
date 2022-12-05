using Database.Models.Base;

namespace Database.Repositories.Interfaces;

/// <summary>
/// Object representing additional select constraints.
/// </summary>
/// <typeparam name="T">Type of entities.</typeparam>
public interface ISearchOptions<T> where T : IDatabaseModel
{
    public int? Limit { get; set; }

    public int? Offset { get; set; }

    public Func<bool, T>? Order { get; set; }
}
