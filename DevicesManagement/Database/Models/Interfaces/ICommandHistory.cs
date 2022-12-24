using Database.Models.Base;

namespace Database.Models.Interfaces;
/// <summary>
/// Represents devices' command history. This entity can only be created and selected.
/// </summary>
public interface ICommandHistory : ICreatableModel
{
    public Command Command { get; }
}
