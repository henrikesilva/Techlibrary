using Microsoft.Extensions.DependencyInjection;
using TechLibrary.Application.AutoMapper;
using TechLibrary.Application.UseCases.Books.Filter;
using TechLibrary.Application.UseCases.Checkouts.Register;
using TechLibrary.Application.UseCases.Login.DoLogin;
using TechLibrary.Application.UseCases.Users.Register;

namespace TechLibrary.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IFilterBooksUseCase, FilterBooksUseCase>();
        services.AddScoped<IRegisterBookCheckoutUseCase, RegisterBookCheckoutUseCase>();
    }
}
