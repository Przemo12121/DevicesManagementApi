using Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace T_Database;

public abstract class DatabaseTest<T> where T : DbContext
{
    protected string Key { get; }
    protected DatabaseTest(string key)
    {
        Key = key;
    }

    protected abstract void EnsureClear(T context);
}

public abstract class DeviceMenagementDatabaseTest : DatabaseTest<DeviceManagementContextTest>
{
    public DeviceMenagementDatabaseTest(string key) : base("T_DeviceManagementDatabase." + key) { }

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
    public LocalAuthDatabaseTest(string key) : base("T_LocalAuthDatabase." + key) { }

    protected override void EnsureClear(LocalAuthContextTest context)
    {
        context.RemoveRange(context.Users);
        context.RemoveRange(context.AccessLevels);
    }
}