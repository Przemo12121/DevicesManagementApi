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


app.Run();

// Allow integration tests
public partial class Program { }