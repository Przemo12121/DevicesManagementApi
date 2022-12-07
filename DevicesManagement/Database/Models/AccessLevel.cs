using Database.Models.Base;
using Database.Models.Enums;
using Database.Models.Interfaces;

namespace Database.Models;

public sealed class AccessLevel : DatabaseModel, IAccessLevel
{
    public string? Description { get; set; }
    public AccessLevels Value { get ; set ; }
}
