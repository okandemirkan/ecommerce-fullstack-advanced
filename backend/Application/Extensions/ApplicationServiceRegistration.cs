using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application.Extensions
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(
            this IServiceCollection services,
            string? autoMapperLicenseKey = null)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
                configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //Bu assembly'deki tüm validator sınıflarını bul, IValidator<T> olarak kaydet.
            services.AddAutoMapper(configAction =>
            {
                if (!string.IsNullOrWhiteSpace(autoMapperLicenseKey))
                    configAction.LicenseKey = autoMapperLicenseKey;
            }, Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
