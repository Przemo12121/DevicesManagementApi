namespace DevicesMenagement.Database.Models
{
    public abstract class DatabaseModel : IDatabaseModel
    {
        public int Id { get; set; }
        public abstract object ToDto();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not IDatabaseModel) return false;

            return this.Id.Equals(((IDatabaseModel)obj).Id);
        }
    }
}
