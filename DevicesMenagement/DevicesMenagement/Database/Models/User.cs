namespace DevicesMenagement.Database.Models
{
    public class User : UpdatableModel, IUser
    {
        /// <summary>
        /// Employee unique identified. Allows to find employee's devices in device database.
        /// </summary>
        public string EmployeeId { get; set; }
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        public bool Enabled { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public override UserDto ToDto()
        {
            return new UserDto();
        }
    }

    public class UserDto
    {

    }
}
