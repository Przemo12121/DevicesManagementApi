namespace DevicesMenagement.Database.Models
{
    public class Command : UpdatableModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Body {  get; set; }

    }
}
