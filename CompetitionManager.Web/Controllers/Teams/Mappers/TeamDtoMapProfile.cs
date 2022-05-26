using AutoMapper;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Web.Controllers.Teams.Dto;

namespace CompetitionManager.Web.Controllers.Teams.Mappers
{
    public class TeamDtoMapProfile : Profile
    {
        public TeamDtoMapProfile()
        {
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.TeamId,
                    opt => opt.MapFrom(src => src.TeamId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TeamLead,
                    opt => opt.MapFrom(src => src.TeamLead))
                .ForMember(dest => dest.Coach,
                    opt => opt.MapFrom(src => src.Coach))
                .ForMember(dest => dest.TeamMates,
                    opt => opt.MapFrom(src => src.TeamMates))
                .ForMember(dest => dest.Results,
                    opt => opt.MapFrom(src => src.Results))
                .ForMember(dest => dest.Competitions,
                    opt => opt.MapFrom(src => src.Competitions))
                .ReverseMap();
            //TODO
        }
    }
}
