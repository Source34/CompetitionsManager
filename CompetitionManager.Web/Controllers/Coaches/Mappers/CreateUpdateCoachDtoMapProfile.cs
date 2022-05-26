using AutoMapper;
using CompetitionManager.Core.Domains.Coaches;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Web.Controllers.Coaches.Dto;
using System.Linq;

namespace CompetitionManager.Web.Controllers.Coaches.Mappers
{
    public class CreateUpdateCoachDtoMapProfile : Profile
    {
        public CreateUpdateCoachDtoMapProfile()
        {

            CreateMap<CreateUpdateCoachDto, Coach>()
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
                    opt => opt.MapFrom(src => src.TrainingTeamsIds.Select(x => new Team() { TeamId = x })));
        }
    }
}
