namespace DevicesMenagement.Database.Models
{
    /// <summary>
    /// Represents devices' command history. This entity can only be created and selected.
    /// </summary>
    public interface ICommandHistory : ICreatableModel
    {
        /// <summary>
        /// Command used.
        /// </summary>
        public Command Command { get; }
    }
}
