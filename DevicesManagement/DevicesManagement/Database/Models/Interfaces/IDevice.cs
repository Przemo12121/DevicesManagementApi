namespace DevicesMenagement.Database.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDevice : IUpdatableModel
    {
        /// <summary>
        /// Device's human-recognizable name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee unique identifier. Refers to a unique user in authorization local database. 
        /// Despite the name, it does not have to be unique, as it is used as foreign key to auth database.
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// List of all device's registered commands. All CRUD operations can be performed on the list.
        /// </summary>
        public List<Command> Commands { get; set; }

        /// <summary>
        /// List of all device commands history. Only selecting entries, and creating new are possible.
        /// </summary>
        public List<CommandHistory> CommandHistory { get; set; }

        /// <summary>
        /// List of all device message history. Only selecting entries, and creating new are possible.
        /// </summary>
        public List<Message> MessageHistory { get; set; }
    }
}
