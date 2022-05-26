using AutoMapper;
using CompetitionManager.Core.Domains.Coaches;

namespace CompetitionManager.Data.Entities.Coaches.Mappers
{
    internal class CoachDbMapProfile : Profile
    {
        public CoachDbMapProfile()
        {
            CreateMap<Coach, CoachDbModel>()
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
                .ReverseMap();
        }
    }
}
