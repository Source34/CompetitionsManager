using AutoMapper;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.TaskResults.Services;
using CompetitionManager.Web.Controllers.TaskResults.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetitionManager.Web.Controllers.TaskResults
{
    [ApiController]
    [Route("[controller]")]
    public class TaskResultController
    {
        private readonly ITaskResultService _taskResultService;
        private readonly IMapper _mapper;

        public TaskResultController(ITaskResultService taskResultService, IMapper mapper)
        {
            _taskResultService = taskResultService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task CreateTaskResult(CreateUpdateTaskResultDto taskResultDto, CancellationToken cancellationToken = default)
        {
            await _taskResultService.CreateAsync(_mapper.Map<TaskResult>(taskResultDto), cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task UpdateTaskResult(int id, CreateUpdateTaskResultDto taskResultDto, CancellationToken cancellationToken = default)
        {
            taskResultDto.TaskResultId = id;
            await _taskResultService.UpdateAsync(_mapper.Map<TaskResult>(taskResultDto), cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTaskResult(int id, CancellationToken cancellationToken = default)
        {
            await _taskResultService.DeleteAsync(id, cancellationToken);
        }

        [HttpGet]
        public async Task<List<TaskResultDto>> GetAllTaskResults(CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<TaskResultDto>>(await _taskResultService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<TaskResultDto> GetTaskResultById(int id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<TaskResultDto>(await _taskResultService.GetByIdAsync(id, cancellationToken));
        }
    }
}
