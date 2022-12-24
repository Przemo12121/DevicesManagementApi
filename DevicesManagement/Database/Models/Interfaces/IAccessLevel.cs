using Database.Models.Base;
using Database.Models.Enums;

namespace Database.Models.Interfaces;

/// <summary>
/// Represents privileges in the system. Cannot be created, nor modified.
/// Used only by the system to authorize access to certain parts of it.
/// </summary>
public interface IAccessLevel : IDatabaseModel
{
    public AccessLevels Value { get; set; }
    public string? Description { get; set; }
}