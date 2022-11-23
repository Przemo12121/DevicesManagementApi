namespace DevicesMenagement.Database.Models
{
    public class Device : UpdatableModel, IDevice
    {

        public string Name { get; set; }
        /// <summary>
        /// Employee unique identified. Refers to unique user in auth database.
        /// </summary>
        public string EmployeeId { get; set; }

        public List<Command> Commands { get; set; } = new List<Command>();
    }
}
