using Database.Models.Base;
using Database.Models.Interfaces;

namespace Database.Models;

public sealed class CommandHistory : CreatableModel, ICommandHistory
{
    public Command Command { get; set; }
}