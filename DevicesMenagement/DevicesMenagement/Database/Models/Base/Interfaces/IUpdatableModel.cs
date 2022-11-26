namespace DevicesMenagement.Database.Models;

/// <summary>
/// Represents database entities, which can be inserted, deleted and updated.
/// </summary>
public interface IUpdatableModel : ICreatableModel
{
    public DateTime UpdatedDate { get; set; }
}
