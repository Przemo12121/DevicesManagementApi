using Database.Models;
using Database.Contexts;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Database.Models.Enums;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class UsersRepository : DisposableRepository<LocalAuthStorageContext>, IUsersRepository, IResourceAuthorizableRepository<User>, IDisposable
{
    public UsersRepository(LocalAuthStorageContext context) : base(context) { }

    public void Add(User entity)
        => _context.Users.Add(entity);

    public void Delete(User entity)
        => _context.Users.Remove(entity);
    public void Update(User entity)
        => _context.Users.Update(entity);

    public List<User> FindAdmins<TOrderKey>(ISearchOptions<User, TOrderKey> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Users
                .Where(user => user.AccessLevel.Value == AccessLevels.Admin)
                .Skip(options.Offset)
                .OrderBy(options.Order)
                .Take(options.Limit)
                .ToList()
            : _context.Users
                .Where(user => user.AccessLevel.Value == AccessLevels.Admin)
                .Skip(options.Offset)
                .OrderByDescending(options.Order)
                .Take(options.Limit)
                .ToList();
    }

    public List<User> FindEmployees<TOrderKey>(ISearchOptions<User, TOrderKey> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Users
                .Where(user => user.AccessLevel.Value == AccessLevels.Employee)
                .Skip(options.Offset)
                .OrderBy(options.Order)
                .Take(options.Limit)
                .ToList()
            : _context.Users
                .Where(user => user.AccessLevel.Value == AccessLevels.Employee)
                .Skip(options.Offset)
                .OrderByDescending(options.Order)
                .Take(options.Limit)
                .ToList();
    }

    public User? FindByEmployeeId(string eid)
    {
       return _context.Users
            .Where(user => user.EmployeeId.Equals(eid))
            .Include(user => user.AccessLevel)
            .SingleOrDefault();
    }

    public User? FindByIdAndOwnerId(Guid id, string ownerId) 
        => id.Equals(ownerId) ? FindById(id) : null;

    public User? FindById(Guid id)
    {
        return _context.Users
            .Where(user => user.Id.Equals(id))
            .SingleOrDefault();
    }
}
