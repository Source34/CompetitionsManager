using AutoMapper;
using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.Tasks;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Web.Controllers.Competitions.Dto;
using System.Linq;

namespace CompetitionManager.Web.Controllers.Competitions.Mappers
{
    public class CreateUpdateCompetitionDtoMapProfile : Profile
    {
        public CreateUpdateCompetitionDtoMapProfile()
        {
            CreateMap<CreateUpdateCompetitionDto, Competition>()
                .ForMember(dest => dest.CompetitionName,
                    opt => opt.MapFrom(src => src.CompetitionName))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartEventDate,
                    opt => opt.MapFrom(src => src.StartEventDate))
                .ForMember(dest => dest.EndEventDate,
                    opt => opt.MapFrom(src => src.EndEventDate))
                .ForMember(dest => dest.Tasks,
                    opt => opt.MapFrom(src => src.Tasks.Select(x => new CompetitionManager.Core.Domains.Tasks.Task() { TaskId = x })))
                .ForMember(dest => dest.Teams,
                    opt => opt.MapFrom(src => src.Teams.Select(x => new Team() { TeamId = x })))
                .ForMember(dest => dest.Results,
                    opt => opt.MapFrom(src => src.Results.Select(x => new TaskResult() { TaskResultId = x })));
        }
    }
}
