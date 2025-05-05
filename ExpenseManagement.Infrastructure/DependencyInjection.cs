using ExpenseManagement.Infrastructure;
using ExpenseManagement.Infrastructure.Data;
using ExpenseManagement.Services.IRepositories;
using ExpenseManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ExpenseManagement.Services.IServices;
using ExpenseManagement.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
             options
             .UseLazyLoadingProxies()
             .UseSqlServer(configuration.GetConnectionString("ExpenseConnection"),
                 builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.RegisterRepositories();

        services.AddScoped<IUserManager, UserManager>();
        services.AddSingleton<PasswordHasherService>();
        services.AddSingleton<JwtTokenService>();

        return services;
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        services.AddScoped<IExpenseDocumentRepository, ExpenseDocumentRepository>();
        services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }
}