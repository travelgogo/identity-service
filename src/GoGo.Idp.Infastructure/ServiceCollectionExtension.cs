using GoGo.Idp.Infastructure.Data;
using GoGo.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoGo.Idp.Infastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddAppInfastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(configuration["Azure:SqlServer:IdentityConnection"]);
            });

            services.AddRepository<IdentityContext>();
        }
        
    }
}