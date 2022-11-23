namespace DevicesMenagement.Database.Models;

public interface ICreatableModel : IDatabaseModel
{
    public DateTime CreatedDate { get; set; }
}
