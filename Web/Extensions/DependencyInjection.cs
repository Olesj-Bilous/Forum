using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string cnxString)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(cnxString));
            return services;
        }
    }
}
