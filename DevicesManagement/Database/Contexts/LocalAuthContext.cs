using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Contexts;

public class LocalAuthContext : DbContext
{
    public LocalAuthContext(DbContextOptions<LocalAuthContext> options) : base(options) { }

    public DbSet<AccessLevel> AccessLevels { get; set; }
    public DbSet<User> Users { get; set; }
}
