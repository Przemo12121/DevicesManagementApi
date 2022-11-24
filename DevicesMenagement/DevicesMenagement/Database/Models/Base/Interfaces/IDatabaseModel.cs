namespace DevicesMenagement.Database.Models;

/// <summary>
/// Represents database entities.
/// </summary>
public interface IDatabaseModel
{
    /// <summary>
    /// Entity's unique identifier. Used as primary key.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Converts databse object to Data Transfer Object
    /// </summary>
    /// <returns>Data Transfer Object</returns>
    public object ToDto();
}
