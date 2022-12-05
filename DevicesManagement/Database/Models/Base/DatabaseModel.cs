namespace Database.Models.Base
{
    public abstract class DatabaseModel : IDatabaseModel
    {
        public Guid Id { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not IDatabaseModel) return false;

            return Id.Equals(((IDatabaseModel)obj).Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
