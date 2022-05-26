using AutoMapper;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Web.Controllers.TaskResults.Dto;

namespace CompetitionManager.Web.Controllers.TaskResults.Mappers
{
    public class TaskResultDtoMapProfile : Profile
    {
        public TaskResultDtoMapProfile()
        {

            CreateMap<TaskResult, TaskResultDto>()
                .ForMember(dest => dest.TaskResultId,
                    opt => opt.MapFrom(src => src.TaskResultId))
                .ForMember(dest => dest.Competition,
                    opt => opt.MapFrom(src => src.Competition))
                .ForMember(dest => dest.Team,
                    opt => opt.MapFrom(src => src.Team))
                .ForMember(dest => dest.Task,
                    opt => opt.MapFrom(src => src.Task))
                .ForMember(dest => dest.ElapsedMinutes,
                    opt => opt.MapFrom(src => src.ElapsedMinutes))
                .ForMember(dest => dest.Result,
                    opt => opt.MapFrom(src => src.Result))
                .ReverseMap();
        }
    }
}
