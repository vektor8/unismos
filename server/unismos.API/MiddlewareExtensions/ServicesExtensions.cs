using Microsoft.EntityFrameworkCore;
using unismos.Data;
using unismos.Data.Repositories;
using unismos.Interfaces.IEnrollment;
using unismos.Interfaces.IPayment;
using unismos.Interfaces.IProfessor;
using unismos.Interfaces.IStudent;
using unismos.Interfaces.ISubject;
using unismos.Interfaces.ITax;
using unismos.Interfaces.ITeaching;
using unismos.Interfaces.IUser;
using unismos.Services.Services;

namespace unismos.API.MiddlewareExtensions;

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

    public static IServiceCollection InjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IProfessorService, ProfessorService>();
        services.AddTransient<ISubjectService, SubjectService>();
        services.AddTransient<ITeachingService, TeachingService>();
        services.AddTransient<IEnrollmentService, EnrollmentService>();
        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<ITaxService, TaxService>();
        services.AddTransient<IPaymentService, PaymentService>();
        return services;
    }

    public static IServiceCollection InjectRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IProfessorRepository, ProfessorRepository>();
        services.AddTransient<ISubjectRepository, SubjectRepository>();
        services.AddTransient<ITeachingRepository, TeachingRepository>();
        services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<ITaxRepository, TaxRepository>();
        return services;
    }
}