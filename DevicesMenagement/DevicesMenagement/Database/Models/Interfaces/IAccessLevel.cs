namespace DevicesMenagement.Database.Models
{
    /// <summary>
    /// Represents privileges in the system. Cannot be created, nor modified.
    /// Used only by the system to authorize access to certain parts of it.
    /// </summary>
    public interface IAccessLevel : IDatabaseModel
    {
        //TODO: change string Name to enum + description
        /// <summary>
        /// Access unique name.
        /// </summary>
        public string Name { get; set; }
    }
}
