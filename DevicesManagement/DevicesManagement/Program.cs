using Database.Contexts;
using Database.Models;
using Database.Repositories;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(assembly);
builder.Services.AddMediatRAttributedBehaviors(assembly);

#region Custom configurations
builder.ConfigureRepositories();   // Database   
builder.ConfigureAuthentication(); // Authentication
builder.ConfigureValidators();     // Request valdiators
builder.ConfigureModelsHandlers(); // Models handlers
#endregion

var app = builder.Build();

// Error routes
app.ConfigureErrorRoutes();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using (var context = new LocalAuthStorageContext())
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
}

app.Run();

// Allow integration tests
public partial class Program { }