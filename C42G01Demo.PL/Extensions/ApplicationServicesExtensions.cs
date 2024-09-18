using BLL.Interfaces;
using BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace C42G01Demo.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepositories>();
            services.AddScoped<IEmploeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
