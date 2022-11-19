namespace DevicesMenagement.Database.Models
{
    public class User : UpdatableModel
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        public bool Enabled { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();
        public AccessLevel AccessLevel { get; set; }
    }
}
