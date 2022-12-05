namespace Database.Models.Base;

/// <summary>
/// Represents database entities.
/// </summary>
public interface IDatabaseModel
{
    public Guid Id { get; init; }
}

/// <summary>
/// Represents database entities, which can be inserted and deleted.
/// </summary>
public interface ICreatableModel : IDatabaseModel
{
    public DateTime CreatedDate { get; set; }
}


/// <summary>
/// Represents database entities, which can be inserted, deleted and updated.
/// </summary>
public interface IUpdatableModel : ICreatableModel
{
    public DateTime UpdatedDate { get; set; }
}
