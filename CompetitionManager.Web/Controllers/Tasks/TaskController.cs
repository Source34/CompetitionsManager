using AutoMapper;
using CompetitionManager.Core.Domains.Tasks.Services;
using CompetitionManager.Web.Controllers.Tasks.Dto;
using CompetitionManager.Web.Controllers.Tasks.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompetitionManager.Web.Controllers.Tasks
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService coachService, IMapper mapper)
        {
            _taskService = coachService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task CreateTask(CreateUpdateTaskDto taskDto, CancellationToken cancellationToken = default)
        {
            await _taskService.CreateAsync(_mapper.Map<CompetitionManager.Core.Domains.Tasks.Task>(taskDto), cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task UpdateTask(int id, CreateUpdateTaskDto taskDto, CancellationToken cancellationToken = default)
        {
            taskDto.TaskId = id;
            await _taskService.UpdateAsync(_mapper.Map<CompetitionManager.Core.Domains.Tasks.Task>(taskDto), cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTask(int id, CancellationToken cancellationToken = default)
        {
            await _taskService.DeleteAsync(id, cancellationToken);
        }

        [HttpGet]
        public async Task<List<TaskDto>> GetAllTasks(CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<TaskDto>>(await _taskService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<TaskDto> GetTaskById(int id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<TaskDto>(await _taskService.GetByIdAsync(id, cancellationToken));
        }

        [HttpGet("byName/{namePart}")]
        public async Task<List<TaskDto>> GetTaskByName(string namePart, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<TaskDto>>(await _taskService.GetByNameAsync(namePart, cancellationToken));
        }
    }
}
