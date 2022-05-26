using CompetitionManager.Core.Domains.Coaches.Services;
using CompetitionManager.Core.Domains.Competitions.Services;
using CompetitionManager.Core.Domains.TaskResults.Services;
using CompetitionManager.Core.Domains.Tasks.Services;
using CompetitionManager.Core.Domains.Teams.Services;
using CompetitionManager.Core.Domains.Users.Services;

using Microsoft.Extensions.DependencyInjection;

namespace CompetitionManager.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskResultService, TaskResultService>();
            services.AddScoped<ICompetitionService, CompetitionService>();
            return services;
        }
    }
}
