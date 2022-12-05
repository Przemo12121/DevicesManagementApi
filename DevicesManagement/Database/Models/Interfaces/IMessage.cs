using Database.Models.Base;

namespace Database.Models.Interfaces;

/// <summary>
/// Represents message sent to device and received from it. This entity can only be created and selected.
/// </summary>
public interface IMessage : ICreatableModel
{
    string Content { get; set; }
}
