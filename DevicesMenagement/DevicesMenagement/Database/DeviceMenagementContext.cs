using Microsoft.EntityFrameworkCore;
using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Database
{
    public class DeviceMenagementContext : DbContext
    {
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Device> Devices { get; set; } 
        public DbSet<DeviceHistory> DevicesHistory { get; set; }
        public DbSet<User> Users { get; set; }

        protected readonly IConfiguration _configuration;

        public DeviceMenagementContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql($"Host=127.0.0.1:6000;Database=devices_menagement;Username=devices;Password=testpassword");
    }
}
