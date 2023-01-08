using Authentication;
using Authentication.Jwt;
using Database.Contexts;
using Database.Models;
using Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

internal static class AppSetup
{
    private static LocalAuthStorageContext LocalAuthStorageContext { get; set; } = new LocalAuthStorageContext();
    public static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LocalAuthStorageContext>();
    }

    public static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        var jwtOptions = new JwtOptions()
        {
            Issuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            Audience = builder.Configuration.GetValue<string>("Jwt:Audience"),
            ExpirationMs = builder.Configuration.GetValue<UInt64>("Jwt:Expiration"),
            Algorithm = builder.Configuration.GetValue<string>("Jwt:Algorithm"),
            Secret = builder.Configuration.GetValue<string>("Jwt:Secret")
        };

        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new()
                {
                    ValidAlgorithms = new[] { jwtOptions.Algorithm },
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Secret)
                        ),
                };
            });

        builder.Services.AddSingleton<IJwtProvider, JwtBearerProvider>(
            service => new JwtBearerProvider(jwtOptions)
        );
        builder.Services.AddSingleton<IIdentityProvider<User>, UserIdentityProvider>(
            service => new UserIdentityProvider(
                new UsersRepository(LocalAuthStorageContext)
            )
        );
    }
}