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
    public DeviceManagementContextTest(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
}

public class LocalAuthStorageContextTest : LocalAuthStorageContext
{
    public LocalAuthStorageContextTest(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
}
