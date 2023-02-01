using Database.Models.Base;

namespace Database.Repositories.Interfaces;

public interface IResourceAuthorizableRepository<TResource> where TResource : IDatabaseModel
{
    public TResource? FindByIdAndOwnerId(Guid id, string ownerId);
    public TResource? FindById(Guid id);
}
