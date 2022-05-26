using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Web.Controllers.Teams.Dto;
using CompetitionManager.Core.Domains.Teams.Services;
using CompetitionManager.Web.Controllers.TaskResults.Dto;
using System.Linq;

namespace CompetitionManager.Web.Controllers.Teams
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task CreateTeam(CreateUpdateTeamDto teamDto, CancellationToken cancellationToken = default)
        {
            await _teamService.CreateAsync(_mapper.Map<Team>(teamDto), cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task UpdateTeam(int id, CreateUpdateTeamDto teamDto, CancellationToken cancellationToken = default)
        {
            teamDto.TeamId = id;
            await _teamService.UpdateAsync(_mapper.Map<Team>(teamDto), cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTeam(int id, CancellationToken cancellationToken = default)
        {
            await _teamService.DeleteAsync(id, cancellationToken);
        }

        [HttpGet]
        public async Task<List<TeamDto>> GetAllTeams(CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<TeamDto>>(await _teamService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<TeamDto> GetTeamById(int id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<TeamDto>(await _teamService.GetByIdAsync(id, cancellationToken));
        }

        [HttpGet("{id}/TaskResults")]
        public async Task<List<TaskResultDto>> GetTaskResultsByTeamId(int id, CancellationToken cancellationToken = default)
        {
            var team = await _teamService.GetByIdAsync(id, cancellationToken);
            var results = team.Results;
            return _mapper.Map<List<TaskResultDto>>(results);
        }

        [HttpGet("{id}/Competition/{competitionId}/TaskResults")]
        public async Task<List<TaskResultDto>> GetTaskResultsByTeamId(int id, int competitionId, CancellationToken cancellationToken = default)
        {
            var results = await _teamService.GetTaskResultsByTeamIdAsync(id, competitionId, cancellationToken);

            return _mapper.Map<List<TaskResultDto>>(results);
        }

        [HttpGet("byName/{namePart}")]
        public async Task<List<TeamDto>> GetTeamByName(string namePart, CancellationToken cancellationToken = default)
        {
            var results = await _teamService.GetByNameAsync(namePart, cancellationToken);

            return _mapper.Map<List<TeamDto>>(results);
        }
    }
}
