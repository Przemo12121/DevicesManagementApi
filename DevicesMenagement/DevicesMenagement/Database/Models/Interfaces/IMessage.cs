namespace DevicesMenagement.Database.Models
{
    /// <summary>
    /// Represents message sent to device and received from it. This entity can only be created and selected.
    /// </summary>
    public interface IMessage : ICreatableModel
    {
        /// <summary>
        /// Actual content of the message.
        /// </summary>
        string Content { get; set; }
    }
}
