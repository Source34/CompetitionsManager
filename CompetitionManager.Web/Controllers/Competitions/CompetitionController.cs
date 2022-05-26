using AutoMapper;
using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.Competitions.Services;
using CompetitionManager.Web.Controllers.Competitions.Dto;
using CompetitionManager.Web.Controllers.TaskResults.Dto;
using CompetitionManager.Web.Controllers.Teams.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetitionManager.Web.Controllers.Competitions
{
    [ApiController]
    [Route("[controller]")]
    public class CompetitionController
    {
        private readonly ICompetitionService _competitionService;
        private readonly IMapper _mapper;

        public CompetitionController(ICompetitionService competiotionService, IMapper mapper)
        {
            _competitionService = competiotionService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<List<CompetitionDto>> GetAllCompetitions(CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<CompetitionDto>>(await _competitionService.GetAllAsync(cancellationToken));
        }

        [HttpGet("results/{id}")]
        public async Task<List<TaskResultDto>> GetCompetitionResults(int id, CancellationToken cancellationToken = default)
        {
            var competion = await _competitionService.GetByIdAsync(id, cancellationToken);
            var results = competion.Results;
            return _mapper.Map<List<TaskResultDto>>(results);
        }
        
        [HttpPost]
        public async Task CreateCompetition(CreateUpdateCompetitionDto competitionDto, CancellationToken cancellationToken = default)
        {
            await _competitionService.CreateAsync(_mapper.Map<Competition>(competitionDto), cancellationToken);
        }
        
        [HttpPut("{id}")]
        public async Task UpdateCompetition(int id, CreateUpdateCompetitionDto competitionDto, CancellationToken cancellationToken = default)
        {
            competitionDto.CompetitionId = id;
            await _competitionService.UpdateAsync(_mapper.Map<Competition>(competitionDto), cancellationToken);
        }
        
        [HttpDelete("{id}")]
        public async Task DeleteCompetition(int id, CancellationToken cancellationToken = default)
        {
            await _competitionService.DeleteAsync(id, cancellationToken);
        }
        
        [HttpGet("{id}")]
        public async Task<CompetitionDto> GetCompetitionById(int id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<CompetitionDto>(await _competitionService.GetByIdAsync(id, cancellationToken));
        }
        
        [HttpGet("{id}/teams")]
        public async Task<List<TeamDto>> GetTeamsByCompetitionId(int id, CancellationToken cancellationToken = default)
        {
            var competion = await _competitionService.GetByIdAsync(id, cancellationToken);
            var results = competion.Teams;
            return _mapper.Map<List<TeamDto>>(results);
        }

        [HttpGet("{id}/Team/{teamId}/TotalPoint")]
        public async Task<double> GetTeamPointsByCompetitonId(int id, int teamId, CancellationToken cancellationToken = default)
        {
            return await _competitionService.GetTeamPointsByCompetitonIdAsync(id, teamId, cancellationToken);

        }
    }
}
