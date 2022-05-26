using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CompetitionManager.Core.Domains.Coaches;
using CompetitionManager.Web.Controllers.Coaches.Dto;
using CompetitionManager.Core.Domains.Coaches.Services;

namespace CompetitionManager.Web.Controllers.Coaches
{
    [ApiController]
    [Route("[controller]")]
    public class CoachController
    {
        private readonly ICoachService _coachService;
        private readonly IMapper _mapper;

        public CoachController(ICoachService coachService, IMapper mapper)
        {
            _coachService = coachService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task CreateCoach(CreateUpdateCoachDto coachDto, CancellationToken cancellationToken = default)
        {
            await _coachService.CreateAsync(_mapper.Map<Coach>(coachDto), cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task UpdateCoach(int id, CreateUpdateCoachDto coachDto, CancellationToken cancellationToken = default)
        {
            coachDto.CoachId = id;
            await _coachService.UpdateAsync(_mapper.Map<Coach>(coachDto), cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCoach(int id, CancellationToken cancellationToken = default)
        {
            await _coachService.DeleteAsync(id, cancellationToken);
        }

        [HttpGet]
        public async Task<List<CoachDto>> GetAllCoaches(CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<CoachDto>>(await _coachService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<CoachDto> GetCoachById(int id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<CoachDto>(await _coachService.GetByIdAsync(id, cancellationToken));
        }
    }
}