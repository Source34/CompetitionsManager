using AutoMapper;
using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.Tasks;
using CompetitionManager.Web.Controllers.Tasks.Dto;
using System.Linq;

namespace CompetitionManager.Web.Controllers.Tasks.Mappers
{
    public class CreateUpdateTaskDtoMapProfile : Profile
    {
        public CreateUpdateTaskDtoMapProfile()
        {

            CreateMap<CreateUpdateTaskDto, Task>()
                .ForMember(dest => dest.TaskId,
                    opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Text,
                    opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Solution,
                    opt => opt.MapFrom(src => src.Solution))
                .ForMember(dest => dest.InputExample,
                    opt => opt.MapFrom(src => src.InputExample))
                .ForMember(dest => dest.OutputExample,
                    opt => opt.MapFrom(src => src.OutputExample))
                .ForMember(dest => dest.Points,
                    opt => opt.MapFrom(src => src.Points))
                .ForMember(dest => dest.Competitions,
                    opt => opt.MapFrom(src => src.Competitions.Select(x => new Competition() { CompetitionId = x })))
                .ForMember(dest => dest.TaskResults,
                    opt => opt.MapFrom(src => src.TaskResults.Select(x => new TaskResult() { TaskResultId = x})))
                .ReverseMap();
        }
    }
}
