namespace DevicesMenagement.Database.Models
{
    public abstract class UpdatableModel : CreatableModel, IUpdatableModel
    {
        /// <summary>
        /// Date and time of last entity edition.
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
