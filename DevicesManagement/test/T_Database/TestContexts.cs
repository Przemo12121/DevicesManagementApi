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

public abstract class DeviceMenagementDatabaseTest : DatabaseTest<DeviceMenagementContext>
{
    public DeviceMenagementDatabaseTest()
        : base(
            new DbContextOptionsBuilder<DeviceMenagementContext>()
                .UseInMemoryDatabase("T_DeviceMenagementDatabase")
                .Options
            )
    {

    }

    protected override void EnsureClear(DeviceMenagementContext context)
    {
        context.RemoveRange(context.Commands);
        context.RemoveRange(context.Devices);
        context.RemoveRange(context.DevicesCommandHistory);
        context.RemoveRange(context.DevicesMessageHistory);
    }
}

public abstract class LocalAuthStorageDatabaseTest : DatabaseTest<LocalAuthStorageContext>
{
    public LocalAuthStorageDatabaseTest()
        : base(
            new DbContextOptionsBuilder<LocalAuthStorageContext>()
                .UseInMemoryDatabase("T_LocalAuthStorageDatabase")
                .Options
            )
    {

    }

    protected override void EnsureClear(LocalAuthStorageContext context)
    {
        context.RemoveRange(context.Users);
        context.RemoveRange(context.AccessLevels);
    }
}