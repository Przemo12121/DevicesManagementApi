using Database.Models.Base;
using Database.Models.Interfaces;

namespace Database.Models;

public class CommandHistory : CreatableModel, ICommandHistory
{
    public Command Command { get; set; }
}