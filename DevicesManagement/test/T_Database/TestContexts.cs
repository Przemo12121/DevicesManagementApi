using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Contexts;

namespace T_Database;

public class DeviceManagementContextTest : DeviceManagementContext
{
    private string Key { get; }
    public DeviceManagementContextTest(string key) 
    {
        Key = key;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase(Key);
}

public class LocalAuthContextTest : LocalAuthStorageContext
{
    private string Key { get; }
    public LocalAuthContextTest(string key)
    {
        Key = key;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseInMemoryDatabase(Key);
}
