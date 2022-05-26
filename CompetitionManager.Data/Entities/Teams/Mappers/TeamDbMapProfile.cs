using AutoMapper;
using CompetitionManager.Core.Domains.Teams;

namespace CompetitionManager.Data.Entities.Teams.Mappers
{
    public class TeamDbMapProfile : Profile
    {
        public TeamDbMapProfile()
        {
            CreateMap<Team, TeamDbModel>()
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
        }
    }
}
