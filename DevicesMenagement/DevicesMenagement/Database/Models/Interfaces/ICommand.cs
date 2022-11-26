namespace DevicesMenagement.Database.Models
{
    /// <summary>
    /// Represents commands that can be applied to regiestered devices. All CRUD operations can be performed on the entities.
    /// </summary>
    public interface ICommand : IUpdatableModel
    {
        /// <summary>
        /// Command's human-recognizable name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Command's optional description.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Command's actual body. This string can be sent to a device.
        /// </summary>
        public string Body { get; set; }
    }
}
