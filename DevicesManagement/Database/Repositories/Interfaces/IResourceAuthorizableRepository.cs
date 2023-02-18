using Database.Models.Base;

namespace Database.Repositories.Interfaces;

public interface IResourceAuthorizableRepository<TResource> where TResource : IDatabaseModel
{
    public Task<TResource?> FindByIdAndOwnerIdAsync(Guid id, string ownerId);
    public Task<TResource?> FindByIdAsync(Guid id);
}
