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
    public DeviceMenagementDatabaseTest(string key)
        : base(
            new DbContextOptionsBuilder<DeviceManagementContextTest>()
                .UseInMemoryDatabase("T_DeviceMenagementDatabase." + key)
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

public abstract class LocalAuthStorageDatabaseTest : DatabaseTest<LocalAuthStorageContextTest>
{
    public LocalAuthStorageDatabaseTest()
        : base(
            new DbContextOptionsBuilder<LocalAuthStorageContextTest>()
                .UseInMemoryDatabase("T_LocalAuthStorageDatabase.")
                .Options
            )
    {

    }

    protected override void EnsureClear(LocalAuthStorageContextTest context)
    {
        context.RemoveRange(context.Users);
        context.RemoveRange(context.AccessLevels);
    }
}