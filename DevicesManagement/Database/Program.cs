using Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Database.Models;

var deviceMenagementConntectionString = args[0];
var authConntectionString = args[1];
var adminEid = args[2];
var password = args[3];

var deviceManagementContext = new DevicesManagementContext(new DbContextOptionsBuilder<DevicesManagementContext>()
                .UseNpgsql(deviceMenagementConntectionString)
                .Options);

var authContext = new LocalAuthContext(
    new DbContextOptionsBuilder<LocalAuthContext>()
                .UseNpgsql(authConntectionString)
                .Options);

deviceManagementContext.Database.Migrate();
authContext.Database.Migrate();

AccessLevel[] levels = { 
    new() { 
        Description = "Admin",
        Id = Guid.NewGuid(),
        Value = Database.Models.Enums.AccessLevels.Admin
    },
    new() {
        Description = "Employee",
        Id = Guid.NewGuid(),
        Value = Database.Models.Enums.AccessLevels.Employee
    },
};
authContext.AccessLevels.AddRange(levels);

User admin = new()
{
    AccessLevel = levels[0],
    CreatedDate = DateTime.UtcNow,
    Enabled = true,
    EmployeeId = adminEid,
    Id = Guid.NewGuid(),
    Name = "admin",
    UpdatedDate = DateTime.UtcNow,
};
admin.PasswordHashed = new PasswordHasher<User>().HashPassword(admin, password);
authContext.Users.Add(admin);
authContext.SaveChanges();