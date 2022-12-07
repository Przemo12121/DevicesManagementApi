using Database.Models.Base;
using Database.Models.Interfaces;

namespace Database.Models;

public sealed class User : UpdatableModel, IUser
{
    public string EmployeeId { get; set; }
    public string Name { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }
    public bool Enabled { get; set; }
    public AccessLevel AccessLevel { get; set; }
}
