using Database.Models.Base;
using Database.Models.Interfaces;

namespace Database.Models;

public sealed class Device : UpdatableModel, IDevice
{
    public string Name { get; set; }
    public string Address { get; set; }
    /// <summary>
    /// Employee unique identified. Refers to unique user in auth database.
    /// </summary>
    public string EmployeeId { get; set; }

    public List<Command> Commands { get; set; }
    public List<Message> Messages { get; set; }
}