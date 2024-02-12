using Authentication;
using Authentication.Jwt;
using Database.Contexts;
using Database.Models;
using Database.Repositories;
using Database.Repositories.Interfaces;
using Database.Repositories.ParallelRepositoryFactories;
using DevicesManagement;
using DevicesManagement.DataTransferObjects.Requests;
using DevicesManagement.Errors;
using DevicesManagement.ModelsHandlers.Factories;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using DevicesManagement.Validations.Authentication;
using DevicesManagement.Validations.Commands;
using DevicesManagement.Validations.Devices;
using DevicesManagement.Validations.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

internal static class WebApplicationBuilderExtensions
{
    public static void ConfigureDatabases(this WebApplicationBuilder builder)
    {

        builder.Services.AddTransient<DbContextOptions<LocalAuthContext>>(serviceProvider =>
            new DbContextOptionsBuilder<LocalAuthContext>()
                .UseNpgsql(builder.Configuration.GetConnectionString("AuthDb"))
                .Options
        );
        builder.Services.AddTransient<DbContextOptions<DevicesManagementContext>>(serviceProvider =>
            new DbContextOptionsBuilder<DevicesManagementContext>()
                .UseNpgsql(builder.Configuration.GetConnectionString("DevicesDb"))
                .Options
        );

        builder.Services.AddDbContext<LocalAuthContext>();
        builder.Services.AddDbContext<DevicesManagementContext>();
    }
    public static void ConfigureRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommandsRepository, CommandsRepository>();
        builder.Services.AddScoped<IDevicesRepository, DevicesRepository>();
        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        builder.Services.AddScoped<IAccessLevelsRepository, AccessLevelsRepository>();

        builder.Services.AddSingleton<IDevicesManagementParallelRepositoriesFactory, DevicesManagementParallelRepositoriesFactory>();
        builder.Services.AddSingleton<ILocalAuthParallelRepositoriesFactory, LocalAuthParallelRepositoriesFactory>();

        builder.Services.AddScoped<IResourceAuthorizableRepository<Command>, CommandsRepository>();
        builder.Services.AddScoped<IResourceAuthorizableRepository<User>, UsersRepository>();
        builder.Services.AddScoped<IResourceAuthorizableRepository<Device>, DevicesRepository>();
    }

    public static void ConfigureModelHandlers(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICommandsFactory<Command>, CommandsFactory>();
        builder.Services.AddSingleton<IDeviceFactory<Device>, DevicesFactory>();

        builder.Services.AddSingleton<ISearchOptionsFactory<Device, string>, DevicesSearchOptionsFactory>();
        builder.Services.AddSingleton<ISearchOptionsFactory<User, string>, UsersSearchOptionsFactory>();
        builder.Services.AddSingleton<ISearchOptionsFactory<Command, string>, CommandsSearchOptionsFactory>();
    }

    public static void ConfigureValidators(this WebApplicationBuilder builder)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;

        builder.Services.AddSingleton<IValidator<LoginWithCredentialsRequest>, LoginWithCredentialsRequestValidator>();

        builder.Services.AddSingleton<IValidator<UpdateCommandRequest>, UpdateCommandRequestValidator>();

        builder.Services.AddSingleton<IValidator<UpdateDeviceRequest>, UpdateDeviceRequestValidator>();
        builder.Services.AddSingleton<IValidator<RegisterCommandRequest>, RegisterCommandRequestValidator>();

        builder.Services.AddSingleton<IValidator<RegisterDeviceRequest>, RegisterDeviceRequestValidator>();
        builder.Services.AddSingleton<IValidator<UpdateEmployeeRequest>, UpdateEmployeeRequestValidator>();
        builder.Services.AddSingleton<IValidator<RegisterEmployeeRequest>, RegisterEmployeeRequestValidator>();
    }

    public static void ConfigureErrorRoutes(this WebApplication app)
    {
        app.UseExceptionHandler(app =>
        {
            app.Run(async (context) =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var error = (ObjectResult)ErrorResponses.CreateDetailed(StatusCodes.Status500InternalServerError, StringMessages.HttpErrors.Details.Internal);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error.Value));
            });
        });
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