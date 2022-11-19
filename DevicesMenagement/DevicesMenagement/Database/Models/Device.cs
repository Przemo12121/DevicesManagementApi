namespace DevicesMenagement.Database.Models
{
    public class Device : UpdatableModel
    {

        public string Name { get; set; }

        public List<Command> Commands { get; set; } = new List<Command>();
    }
}
