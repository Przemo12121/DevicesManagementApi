namespace Database.Models.Base
{
    public abstract class UpdatableModel : CreatableModel, IUpdatableModel
    {
        public DateTime UpdatedDate { get; set; }
    }
}
