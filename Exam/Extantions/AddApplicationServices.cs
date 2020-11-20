using Microsoft.Extensions.DependencyInjection;
using Repository.Common;

namespace Exam.Extantions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
           
            return services;
        }
    }
}
