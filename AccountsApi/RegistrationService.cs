
using Context.Context;
using Core.Services.Contracts;
using Core.Services.Implementations;
using Infrastructure.Mappings;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AccountsApi;

public static class RegistrationService
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.RegisterDatabaseServices();
        builder.RegisterRepositories();
        builder.RegisterApplicationServices();
    }

    private static void RegisterDatabaseServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AccountsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
    }

    private static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }

    private static void RegisterApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Mappings));
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
    }
}
