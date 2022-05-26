using AutoMapper;
using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.Tasks;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Web.Controllers.TaskResults.Dto;

namespace CompetitionManager.Web.Controllers.TaskResults.Mappers
{
    public class CreateUpdateTaskResultDtoMapProfile : Profile
    {
        public CreateUpdateTaskResultDtoMapProfile()
        {
            CreateMap<CreateUpdateTaskResultDto, TaskResult>()
                .ForMember(dest => dest.TaskResultId,
                    opt => opt.MapFrom(src => src.TaskResultId))
                .ForMember(dest => dest.Competition,
                    opt => opt.MapFrom(src => new Competition() { CompetitionId = src.CompetitionId}))
                .ForMember(dest => dest.Team,
                    opt => opt.MapFrom(src => new Team() { TeamId = src.TeamId }))
                .ForMember(dest => dest.Task,
                    opt => opt.MapFrom(src => new Task() { TaskId = src.TaskId }))
                .ForMember(dest => dest.ElapsedMinutes,
                    opt => opt.MapFrom(src => src.ElapsedMinutes))
                .ForMember(dest => dest.Result,
                    opt => opt.MapFrom(src => src.Result))
                .ReverseMap();
        }
    }
}
