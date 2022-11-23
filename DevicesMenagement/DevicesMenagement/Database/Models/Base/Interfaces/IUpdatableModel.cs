namespace DevicesMenagement.Database.Models;

public interface IUpdatableModel : ICreatableModel
{
    public DateTime UpdatedDate { get; set; }
}
