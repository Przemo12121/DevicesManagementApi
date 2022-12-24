using Database.Models.Base;
using Database.Models.Interfaces;

namespace Database.Models;

public sealed class Message : CreatableModel, IMessage
{
    public string Content { get; set ; }
    public string From { get; set; }
    public string To { get; set; }
}
