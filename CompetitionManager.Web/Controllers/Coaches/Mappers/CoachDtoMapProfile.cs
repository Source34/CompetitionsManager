using AutoMapper;
using CompetitionManager.Core.Domains.Coaches;
using CompetitionManager.Web.Controllers.Coaches.Dto;

namespace CompetitionManager.Web.Controllers.Coaches.Mappers
{
    public class CoachDtoMapProfile : Profile
    {
        public CoachDtoMapProfile()
        {

            CreateMap<Coach, CoachDto>()
                .ForMember(dest => dest.CoachId,
                    opt => opt.MapFrom(src => src.CoachId))
                .ForMember(dest => dest.CodeforcesLogin,
                    opt => opt.MapFrom(src => src.CodeforcesLogin))
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname,
                    opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Patronymic,
                    opt => opt.MapFrom(src => src.Patronymic))
                .ForMember(dest => dest.AcademicDegree,
                    opt => opt.MapFrom(src => src.AcademicDegree))
                .ForMember(dest => dest.University,
                    opt => opt.MapFrom(src => src.University))
                .ForMember(dest => dest.TrainingTeams,
                    opt => opt.MapFrom(src => src.TrainingTeams))
                .ReverseMap();
        }
    }
}
