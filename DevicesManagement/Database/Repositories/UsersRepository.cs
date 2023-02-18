using Database.Contexts;
using Database.Models;
using Database.Models.Enums;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

    public Task<List<User>> FindAdminsAsync<TOrderKey>(ISearchOptions<User, TOrderKey> options)
    {
        var query = _context.Users
                .Where(user => user.AccessLevel.Value.Equals(AccessLevels.Admin));

        var orderedQuery = options.OrderDirection.Equals(OrderDirections.ASCENDING)
            ? query.OrderBy(options.Order)
            : query.OrderByDescending(options.Order);

        return orderedQuery.Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();
    }

    public Task<List<User>> FindEmployeesAsync<TOrderKey>(ISearchOptions<User, TOrderKey> options)
    {
        var query = _context.Users
                .Where(user => user.AccessLevel.Value.Equals(AccessLevels.Employee));

        var orderedQuery = options.OrderDirection.Equals(OrderDirections.ASCENDING)
            ? query.OrderBy(options.Order)
            : query.OrderByDescending(options.Order);

        return orderedQuery.Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();
    }
    public Task<int> CountEmployeesAsync()
        => _context.Users
            .CountAsync(user => user.AccessLevel.Value.Equals(AccessLevels.Employee));

    public Task<User?> FindByEmployeeIdAsync(string eid)
     => _context.Users
            .Where(user => user.EmployeeId.Equals(eid))
            .Include(user => user.AccessLevel)
            .FirstOrDefaultAsync();

    public Task<User?> FindByIdAndOwnerIdAsync(Guid id, string ownerId) 
        => id.Equals(ownerId) 
            ? FindByIdAsync(id) 
            : Task.FromResult<User?>(null);

    public Task<User?> FindByIdAsync(Guid id)
       => _context.Users
            .Where(user => user.Id.Equals(id))
            .FirstOrDefaultAsync();
}
