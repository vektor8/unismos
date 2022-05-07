using Microsoft.EntityFrameworkCore;
using unismos.Data;

public static class ServicesExtensions
{
    public static IServiceCollection DbOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<DataContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("constr")));

        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        dbContext.Database.Migrate();
        return services;
    }
}