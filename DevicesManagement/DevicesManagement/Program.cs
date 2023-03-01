using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using System.Reflection;

using Database.Contexts;
using Database.Models;
using Database.Repositories;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var assembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(assembly);
builder.Services.AddMediatRAttributedBehaviors(assembly);

builder.ConfigureDatabases();
builder.ConfigureRepositories();
builder.ConfigureAuthentication();
builder.ConfigureValidators();
builder.ConfigureModelHandlers();

var app = builder.Build();

app.ConfigureErrorRoutes();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//// THIS IS IN USE AS TEMPORARY UTILITY !
/*using (var context = new LocalAuthContext(
    new DbContextOptionsBuilder<LocalAuthContext>()
        .UseNpgsql("Server=auth_db;Database=devices_menagement_auth;Username=devices_auth;Password=testpassword_auth")
            .Options
        ))
{
    AccessLevel a = new()
    {
        Value = Database.Models.Enums.AccessLevels.Admin,
        Id = Guid.NewGuid()
    };
    User u = new()
    {
        EmployeeId = "aaaa12345678",
        AccessLevelId = a.Id,
        Name = "aaa",
        Id = Guid.NewGuid(),
    };
    var pwd = new PasswordHasher<User>().HashPassword(u, "testPWD1");
    u.PasswordHashed = pwd;
    context.AccessLevels.Add(a);
    context.Users.Add(u);
    context.SaveChanges();
}*/

app.InitDb();
app.Run();

// Allow integration tests
public partial class Program { }