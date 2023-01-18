using Database.Models.Base;

namespace Database.Repositories.Interfaces;

public interface IResourceAuthorizableRepository<TResource> where TResource : IDatabaseModel
{
    public TResource? FindByIdAndEmployeeId(Guid id, string employeeId);
    public TResource? FindById(Guid id);
}
