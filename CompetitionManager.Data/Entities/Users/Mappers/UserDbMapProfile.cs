using AutoMapper;
using CompetitionManager.Core.Domains.Users;

namespace CompetitionManager.Data.Entities.Users.Mappers
{
    public class UserDbMapProfile : Profile
    {
        public UserDbMapProfile()
        {
            CreateMap<User, UserDbModel>()
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
                .ForMember(dest => dest.GradebookNumber,
                    opt => opt.MapFrom(src => src.GradebookNumber))
                .ReverseMap();
        }
    }
}
