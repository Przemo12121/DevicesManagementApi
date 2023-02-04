using Database.Models;
namespace DevicesManagement.MediatR.Requests;

public interface IAccessLevelContainer
{
    AccessLevel AccessLevel { get; set;  }
}
