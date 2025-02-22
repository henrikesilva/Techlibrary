using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechLibrary.Domain.Repositories;
using TechLibrary.Domain.Repositories.Books;
using TechLibrary.Domain.Repositories.Checkouts;
using TechLibrary.Domain.Repositories.User;
using TechLibrary.Domain.Security.Cryptography;
using TechLibrary.Domain.Security.Tokens.Access;
using TechLibrary.Domain.Services.LoggedUser;
using TechLibrary.Infraestructure.DataAccess;
using TechLibrary.Infraestructure.DataAccess.Repositories;
using TechLibrary.Infraestructure.Security.Cryptography;
using TechLibrary.Infraestructure.Security.Tokens.Access;
using TechLibrary.Infraestructure.Services.LoggedUser;

namespace TechLibrary.Infraestructure;
public static class DependencyInjection
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddToken(services, configuration);
        AddDbContext(services, configuration);

        services.AddScoped<IBCryptAlgorithm, BCryptAlgorithm>();
        services.AddScoped<ILoggedUserServices, LoggedUserServices>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
     
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

        services.AddScoped<IBookReadOnlyRepository, BookRepository>();
        services.AddScoped<IBookWriteOnlyRepository, BookRepository>();

        services.AddScoped<ICheckoutReadOnlyRepository, CheckoutRepository>();
        services.AddScoped<ICheckoutWriteOnlyRepository, CheckoutRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<TechLibraryDbContext>(config =>
        {
            config.UseSqlite(connectionString);
        });
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        services.AddScoped<IJwtTokenGenerator>(config => new JwtTokenGenerator(signingKey!));
    }
}
