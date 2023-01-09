using MediatR;
using MediatR.Extensions.AttributedBehaviors;
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(assembly);
builder.Services.AddMediatRAttributedBehaviors(assembly);

// Database
AppSetup.ConfigureDatabase(builder);

// Authentication
AppSetup.ConfigureAuthentication(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Allow integration tests
public partial class Program { }