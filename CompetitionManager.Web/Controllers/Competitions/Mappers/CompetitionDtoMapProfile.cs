using AutoMapper;
using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Web.Controllers.Competitions.Dto;

namespace CompetitionManager.Web.Controllers.Competitions.Mappers
{
    public class CompetitionDtoMapProfile : Profile
    {
        public CompetitionDtoMapProfile()
        {
            CreateMap<Competition, CompetitionDto>()
                .ForMember(dest => dest.CompetitionId,
                    opt => opt.MapFrom(src => src.CompetitionId))
                .ForMember(dest => dest.CompetitionName,
                    opt => opt.MapFrom(src => src.CompetitionName))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartEventDate,
                    opt => opt.MapFrom(src => src.StartEventDate))
                .ForMember(dest => dest.EndEventDate,
                    opt => opt.MapFrom(src => src.EndEventDate))
                .ForMember(dest => dest.Tasks,
                    opt => opt.MapFrom(src => src.Tasks))
                .ForMember(dest => dest.Teams,
                    opt => opt.MapFrom(src => src.Teams))
                .ForMember(dest => dest.Results,
                    opt => opt.MapFrom(src => src.Results))

                .ReverseMap();
        }
    }
}
