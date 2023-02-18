using Authentication;
using Authentication.Jwt;
using Database.Contexts;
using Database.Models;
using Database.Repositories;
using Database.Repositories.Interfaces;
using Database.Repositories.ParallelRepositoryFactories;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.ModelsHandlers.Factories;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using DevicesManagement.Validations.Authentication;
using DevicesManagement.Validations.Commands;
using DevicesManagement.Validations.Devices;
using DevicesManagement.Validations.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal static class WebApplicationBuilderExtensions
{
    public static void ConfigureRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LocalAuthStorageContext>();
        builder.Services.AddDbContext<DevicesManagementContext>();

        #region General repositories
        builder.Services.AddScoped<ICommandsRepository, CommandsRepository>();
        builder.Services.AddScoped<IDevicesRepository, DevicesRepository>();
        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        builder.Services.AddScoped<IAccessLevelsRepository, AccessLevelsRepository>();
        #endregion

        #region Parallel repositories factories
        builder.Services.AddSingleton<IDevicesManagementParallelRepositoriesFactory, DevicesManagementParallelRepositoriesFactory>();
        builder.Services.AddSingleton<ILocalAuthParallelRepositoriesFactory, LocalAuthParallelRepositoriesFactory>();
        #endregion

        #region IAuthorizable<T> repositories
        builder.Services.AddScoped<IResourceAuthorizableRepository<Command>, CommandsRepository>();
        builder.Services.AddScoped<IResourceAuthorizableRepository<User>, UsersRepository>();
        builder.Services.AddScoped<IResourceAuthorizableRepository<Device>, DevicesRepository>();
        #endregion
    }

    public static void ConfigureModelsHandlers(this WebApplicationBuilder builder)
    {
        #region Factories
        builder.Services.AddSingleton<ICommandsFactory<Command>, CommandsFactory>();
        builder.Services.AddSingleton<IDeviceFactory<Device>, DevicesFactory>();

        builder.Services.AddSingleton<ISearchOptionsFactory<Device, string>, DevicesSearchOptionsFactory>();
        builder.Services.AddSingleton<ISearchOptionsFactory<User, string>, UsersSearchOptionsFactory>();
        builder.Services.AddSingleton<ISearchOptionsFactory<Command, string>, CommandsSearchOptionsFactory>();
        #endregion
    }

    public static void ConfigureValidators(this WebApplicationBuilder builder)
    {
        #region Authentication
        builder.Services.AddSingleton<IValidator<LoginWithCredentialsRequest>, LoginWithCredentialsRequestValidator>();
        #endregion
        #region Commands
        builder.Services.AddSingleton<IValidator<UpdateCommandRequest>, UpdateCommandRequestValidator>();
        #endregion
        #region Devices
        builder.Services.AddSingleton<IValidator<UpdateDeviceRequest>, UpdateDeviceRequestValidator>();
        builder.Services.AddSingleton<IValidator<RegisterCommandRequest>, RegisterCommandRequestValidator>();
        #endregion
        #region Users
        builder.Services.AddSingleton<IValidator<RegisterDeviceRequest>, RegisterDeviceRequestValidator>();
        builder.Services.AddSingleton<IValidator<UpdateEmployeeRequest>, UpdateEmployeeRequestValidator>();
        builder.Services.AddSingleton<IValidator<RegisterEmployeeRequest>, RegisterEmployeeRequestValidator>();
        #endregion
    }

    public static void ConfigureErrorRoutes(this WebApplication app)
    {
        //app.UseMiddleware<HttpExceptionHandler>();
    }

    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        JwtOptions jwtOptions = new()
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
        builder.Services.AddScoped<IIdentityProvider<User>, UserIdentityProvider>();
    }
}