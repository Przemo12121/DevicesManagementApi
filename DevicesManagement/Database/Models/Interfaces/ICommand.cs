using Database.Models.Base;

namespace Database.Models.Interfaces;

/// <summary>
/// Represents commands that can be applied to regiestered devices. All CRUD operations can be performed on the entities.
/// </summary>
public interface ICommand : IUpdatableModel
{
    public string Name { get; set; }
        
    public string? Description { get; set; }
        
    public string Body { get; set; }
 }