using Database.Models.Base;

namespace Database.Models.Interfaces;

/// <summary>
/// Represents user in the system. All CRUD operations can be perfomed on the entities.
/// </summary>
public interface IUser : IUpdatableModel
{
    public string EmployeeId { get; set; }

    public string Name { get; set; }

    public string PasswordHashed { get; set; }

    public bool Enabled { get; set; }

    public AccessLevel AccessLevel { get; set; }
}
