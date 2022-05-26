using AutoMapper;
using CompetitionManager.Core.Domains.Users;
using CompetitionManager.Web.Controllers.Users.Dto;

namespace CompetitionManager.Web.Controllers.Users.Mappers
{
    public class UserDtoMapProfile : Profile
    {
        public UserDtoMapProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CodeforcesLogin,
                    opt => opt.MapFrom(src => src.CodeforcesLogin))
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname,
                    opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Patronymic,
                    opt => opt.MapFrom(src => src.Patronymic))
                .ForMember(dest => dest.FromVstu,
                    opt => opt.MapFrom(src => src.FromVstu))
                .ForMember(dest => dest.University,
                    opt => opt.MapFrom(src => src.University))
                .ForMember(dest => dest.TeamsMemberships,
                    opt => opt.MapFrom(src => src.TeamsMemberships))
                .ForMember(dest => dest.LeadedTeams,
                    opt => opt.MapFrom(src => src.LeadedTeams))
                .ForMember(dest => dest.GradebookNumber,
                    opt => opt.MapFrom(src => src.GradebookNumber))
                .ReverseMap();
        }
    }
}
