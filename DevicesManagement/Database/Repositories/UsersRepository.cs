using Database.Models;
using Database.Contexts;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Database.Models.Enums;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class UsersRepository : DisposableRepository<LocalAuthStorageContext>, IUsersRepository<User>, IDisposable
{
    public UsersRepository(LocalAuthStorageContext context) : base(context) { }

    public void Add(User entity)
    {
        _context.Users.Add(entity);
        _context.SaveChanges();
    }

    public void Delete(User entity)
    {
        _context.Users.Remove(entity);
        _context.SaveChanges();
    }
    public void Update(User entity)
    {
        _context.Users.Update(entity);
        _context.SaveChanges();
    }

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
            .SingleOrDefault();
    }
}
