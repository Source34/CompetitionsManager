using CompetitionManager.Core.Domains.Coaches.Repositories;
using CompetitionManager.Core.Domains.Competitions.Repositories;
using CompetitionManager.Core.Domains.TaskResults.Repositories;
using CompetitionManager.Core.Domains.Tasks.Repositories;
using CompetitionManager.Core.Domains.Teams.Repositories;
using CompetitionManager.Core.Domains.Users.Repositories;
using CompetitionManager.Core.Seriveces;
using CompetitionManager.Data.Contexts;
using CompetitionManager.Data.Entities.Coaches.Repository;
using CompetitionManager.Data.Entities.Competitions.Repository;
using CompetitionManager.Data.Entities.TaskResults.Repository;
using CompetitionManager.Data.Entities.Tasks.Repository;
using CompetitionManager.Data.Entities.Teams.Repository;
using CompetitionManager.Data.Entities.Users.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompetitionManager.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICoachRepository, CoachRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ICompetitionRepository, CompetitionRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskResultRepository, TaskResultRepository>();

            services.AddScoped<IUnitOfWork, CompetitionUnitOfWork>();

            services.AddDbContext<CompetitionDbContext>(options => options
                    .UseSqlServer(configuration["ConnectionString"])
                    .UseCamelCaseNamingConvention());

            return services;
        }

    }
}
