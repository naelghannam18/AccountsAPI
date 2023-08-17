using Application.Behaviors;
using Domain.Configurations;
using FluentValidation;
using Infrastructure.Mappings;
using Infrastructure.Repositories.Contracts;
using Infrastructure.Repositories.Implementations;
using MediatR;
namespace AccountsApi;

public static class RegistrationService
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();;
        builder.Services.AddSwaggerGen();

        builder.RegisterConfigurationClasses();
        builder.RegisterDatabaseServices();
        builder.RegisterRepositories();
        builder.RegisterApplicationServices();
    }

    private static void RegisterDatabaseServices(this WebApplicationBuilder builder)
    {
    }

    private static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
        builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
        builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();

    }

    private static void RegisterApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.ApplicationAssemblyReference);
        });
        builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        builder.Services.AddValidatorsFromAssembly(Application.AssemblyReference.ApplicationAssemblyReference, includeInternalTypes: true);
        builder.Services.AddAutoMapper(typeof(Mappings));
    }

    private static void RegisterConfigurationClasses(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection("MongoDB"));
    }
}
