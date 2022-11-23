namespace DevicesMenagement.Database.Models
{
    public class UpdatableModel : CreatableModel, IUpdatableModel
    {
        public DateTime UpdatedDate { get; set; }
    }
}
