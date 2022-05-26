using System;
using CompetitionManager.Web.HostedService;
using Microsoft.Extensions.DependencyInjection;

namespace CompetitionManager.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddWeb(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddHostedService<MigrationHostedService>();
            return services;
        }
    }
}
