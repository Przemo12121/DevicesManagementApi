namespace DevicesMenagement.Database.Models
{
    public abstract class CreatableModel : DatabaseModel, ICreatableModel
    {
        /// <summary>
        /// Date and time of entity creation.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
