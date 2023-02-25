using Microsoft.EntityFrameworkCore;
using Database.Contexts;

namespace T_Database;

public class DeviceManagementContextTest : DevicesManagementContext
{
    private string Key { get; }
    public DeviceManagementContextTest(string key) 
    {
        Key = key;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase(Key);
}

public class LocalAuthContextTest : LocalAuthContext
{
    static DbContextOptionsBuilder<LocalAuthContext> optionsBuilder = new();
    public LocalAuthContextTest(string key) : base(optionsBuilder.UseInMemoryDatabase(key).Options)
    {
    }
}
