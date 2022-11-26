namespace DevicesMenagement.Database.Models;

/// <summary>
/// Represents database entities, which can be inserted and deleted.
/// </summary>
public interface ICreatableModel : IDatabaseModel
{
    public DateTime CreatedDate { get; set; }
}
