namespace DevicesMenagement.Database.Models
{
    public class CreatableModel : DatabaseModel, ICreatableModel
    {
        public DateTime CreatedDate { get; set; }
    }
}
