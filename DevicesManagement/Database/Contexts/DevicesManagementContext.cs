using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database.Contexts;

public class DevicesManagementContext : DbContext
{
    public DevicesManagementContext(DbContextOptions<DevicesManagementContext> options) : base(options) { }

    public DbSet<Command> Commands { get; set; }
    public DbSet<Device> Devices { get; set; } 
    public DbSet<CommandHistory> DevicesCommandHistory { get; set; }
    public DbSet<Message> DevicesMessageHistory { get; set; }

    public void Init()
    {
        Database.Migrate();
    }
}
