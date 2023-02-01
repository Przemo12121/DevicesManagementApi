using Database.Models.Base;

namespace DevicesManagement.MediatR.Commands;

public interface IResourceAuthorizableCommand<TResource> where TResource : IDatabaseModel
{
    public Guid ResourceId { get; init; }
    public TResource Resource { get; set; }
}
