namespace DevicesMenagement.Database.Models
{
    /// <summary>
    /// Represents user in the system. All CRUD operations can be perfomed on the entities.
    /// </summary>
    public interface IUser : IUpdatableModel
    {
        /// <summary>
        /// Employee unique identified. Allows to find employee's devices in device database. Used as user's login.
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// User's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User's hashed password, used for authorization purposes.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Value specifying whether user is active in the system.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Represents user's privileges in the system.
        /// </summary>
        public AccessLevel AccessLevel { get; set; }
    }
}
