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

/*public partial class Program
{
    record X(string A, string B);
    record X2(string A, List<string> B);

    public static void Main(string[] args)
    {

        var x = new List<X>(new[]
        {
            new X("abc", "hola"),
            new X("xd", "!&"),
            new X("abc", "aaaola2"),
            new X("xd", "___2"),
            new X("123", "ooooo")
        });

        var z = x.GroupBy(a => a.A).Select(g => new X2(g.First().A, g.Select(g => g.B).ToList()));

        Console.WriteLine(JsonContent.Create(x));
    }
}
*/