using DevicesMenagement.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevicesMenagement.Database
{
    public class LocalAuthStorageContext : DbContext
    {
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //TODO change postgres database to lighter, much more appropriate database system for storing credentials
            => optionsBuilder.UseNpgsql($"Host=127.0.0.1:6001;Database=devices_menagement_auth;Username=devices_auth;Password=testpassword_auth");
    }
}
