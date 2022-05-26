using AutoMapper;

namespace CompetitionManager.Data.Entities.Tasks.Mappers
{
    internal class TaskDbMapProfile : Profile
    {
        public TaskDbMapProfile()
        {
            CreateMap<CompetitionManager.Core.Domains.Tasks.Task, TaskDbModel>()
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
                    opt => opt.MapFrom(src => src.Competitions))
                .ForMember(dest => dest.TaskResults,
                    opt => opt.MapFrom(src => src.TaskResults))
                .ReverseMap();
        }
    }
}
