using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Domain.Enums;
using School.Domain.Repositories;
using School.Domain.Repositories.Academy;
using School.Domain.Repositories.Student;
using School.Domain.Repositories.Token;
using School.Domain.Repositories.User;
using School.Domain.Security.Cryptography;
using School.Domain.Security.Tokens;
using School.Domain.Services.LoggedUser;
using School.Infrastructure.DataAccess;
using School.Infrastructure.DataAccess.Repositories;
using School.Infrastructure.Extensions;
using School.Infrastructure.Security.Cryptography;
using School.Infrastructure.Security.Tokens.Access.Generator;
using School.Infrastructure.Security.Tokens.Access.Validator;
using School.Infrastructure.Security.Tokens.Refresh;
using School.Infrastructure.Services.LoggedUser;
using System.Reflection;

namespace School.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPasswordEncrpter(services);
        AddRepositories(services);
        AddLoggedUser(services);
        AddTokens(services, configuration);

        if (configuration.IsUnitTestEnviroment())
            return;

        var databaseType = configuration.DatabaseType();

        if (databaseType == DatabaseType.SqlServer)
        {
            AddDbContext_SqlServer(services, configuration);
            AddFluentMigrator_SqlServer(services, configuration);
        }
    }

    private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnetionString();

        services.AddDbContext<SchoolDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        services.AddScoped<IAcademyWriteOnlyRepository, AcademyRepository>();
        services.AddScoped<IAcademyReadOnlyRepository, AcademyRepository>();
        services.AddScoped<IAcademyUpdateOnlyRepository, AcademyRepository>();
        services.AddScoped<IStudentWriteOnlyRepository, StudentRepository>();
        services.AddScoped<IStudentReadOnlyRepository, StudentRepository>();
        services.AddScoped<IStudentUpdateOnlyRepository, StudentRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
    }

    private static void AddFluentMigrator_SqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnetionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("School.Infrastructure")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));

        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

    private static void AddPasswordEncrpter(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncripter, BCryptNet>();
    }
}