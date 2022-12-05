using Database.Models.Base;
using Database.Models.Interfaces;

namespace Database.Models;

public class Command : UpdatableModel, ICommand
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Body {  get; set; }
}
