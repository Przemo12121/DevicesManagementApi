using Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace T_Database;

public abstract class DatabaseTest<T> where T : DbContext
{
    protected DbContextOptions<T> ContextOptions { get; }
    protected DatabaseTest(DbContextOptions<T> contextOptions)
    {
        ContextOptions = contextOptions;
    }


    protected abstract void EnsureClear(T context);
}

public abstract class DeviceMenagementDatabaseTest : DatabaseTest<DeviceManagementContextTest>
{
    public DeviceMenagementDatabaseTest(string methodKey)
        : base(
            new DbContextOptionsBuilder<DeviceManagementContextTest>()
                .UseInMemoryDatabase("T_DeviceMenagementDatabase." + methodKey)
                .Options
            )
    {

    }

    protected override void EnsureClear(DeviceManagementContextTest context)
    {
        context.RemoveRange(context.Commands);
        context.RemoveRange(context.Devices);
        context.RemoveRange(context.DevicesCommandHistory);
        context.RemoveRange(context.DevicesMessageHistory);
    }
}

public abstract class LocalAuthDatabaseTest : DatabaseTest<LocalAuthContextTest>
{
    public LocalAuthDatabaseTest(string methodKey)
        : base(
            new DbContextOptionsBuilder<LocalAuthContextTest>()
                .UseInMemoryDatabase("T_LocalAuthStorageDatabase." + methodKey)
                .Options
            )
    {

    }

    protected override void EnsureClear(LocalAuthContextTest context)
    {
        context.RemoveRange(context.Users);
        context.RemoveRange(context.AccessLevels);
    }
}