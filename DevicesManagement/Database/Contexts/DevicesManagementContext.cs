using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database.Contexts;

public class DevicesManagementContext : DbContext
{
    public DbSet<Command> Commands { get; set; }
    public DbSet<Device> Devices { get; set; } 
    public DbSet<CommandHistory> DevicesCommandHistory { get; set; }
    public DbSet<Message> DevicesMessageHistory { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql($"Host=127.0.0.1:6000;Database=devices_menagement;Username=devices;Password=testpassword");
            //.LogTo(Console.WriteLine);
}
