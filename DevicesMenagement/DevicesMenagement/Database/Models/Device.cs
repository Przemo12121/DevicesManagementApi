namespace DevicesMenagement.Database.Models
{
    public class Device : UpdatableModel, IDevice
    {

        public string Name { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// Employee unique identified. Refers to unique user in auth database.
        /// </summary>
        public string EmployeeId { get; set; }

        public List<Command> Commands { get; set; }
        public List<CommandHistory> CommandHistory { get; set; }
        public List<Message> MessageHistory { get; set; }

        public override object ToDto()
        {
            return new DeviceDto();
        }
    }

    public class DeviceDto
    {

    }
}
