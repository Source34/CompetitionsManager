using AutoMapper;
using CompetitionManager.Core.Domains.Coaches;
using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Core.Domains.Users;
using CompetitionManager.Web.Controllers.Teams.Dto;
using System.Linq;

namespace CompetitionManager.Web.Controllers.Teams.Mappers
{
    public class CreateUpdateTeamDtoMapProfile : Profile
    {
        public CreateUpdateTeamDtoMapProfile()
        {
            CreateMap<CreateUpdateTeamDto, Team>()

                .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.TeamId))

                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))

                .ForMember(dest => dest.TeamLead, opt => opt.MapFrom(src => new User() { UserId = src.TeamLeadId }))

                .ForMember(dest => dest.Coach, opt => opt.MapFrom(src => new Coach() { CoachId = src.CoachId }))

                .ForMember(dest => dest.TeamMates, opt => opt.MapFrom(src => src.TeamMatesIds.Select(x => new User() { UserId = x })))

                .ForMember(dest => dest.Results, opt => opt.MapFrom(src => src.Results.Select(x => new TaskResult() { TaskResultId = x })))

                .ForMember(dest => dest.Competitions, opt => opt.MapFrom(src => src.Competitions.Select(x => new Competition() { CompetitionId = x })));
        }
    }
}
