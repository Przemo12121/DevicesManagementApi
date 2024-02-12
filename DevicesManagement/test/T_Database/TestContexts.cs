using Microsoft.EntityFrameworkCore;
using Database.Contexts;

namespace T_Database;

public class DeviceManagementContextTest : DevicesManagementContext
{
    static DbContextOptionsBuilder<DevicesManagementContext> optionsBuilder = new();
    public DeviceManagementContextTest(string key) : base(optionsBuilder.UseInMemoryDatabase(key).Options)
    {
    }
}

public class LocalAuthContextTest : LocalAuthContext
{
    static DbContextOptionsBuilder<LocalAuthContext> optionsBuilder = new();
    public LocalAuthContextTest(string key) : base(optionsBuilder.UseInMemoryDatabase(key).Options)
    {
    }
}
