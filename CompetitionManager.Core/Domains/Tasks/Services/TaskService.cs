using CompetitionManager.Core.Domains.Tasks.Repositories;
using CompetitionManager.Core.Domains.Tasks;
using CompetitionManager.Core.Seriveces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionManager.Core.Domains.Tasks.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        public System.Threading.Tasks.Task<CompetitionManager.Core.Domains.Tasks.Task> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _taskRepository.GetByIdAsync(id, cancellationToken);
        }

        public System.Threading.Tasks.Task<List<CompetitionManager.Core.Domains.Tasks.Task>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _taskRepository.GetAllAsync(cancellationToken);
        }

        public async System.Threading.Tasks.Task CreateAsync(CompetitionManager.Core.Domains.Tasks.Task task, CancellationToken cancellationToken)
        {
            await _taskRepository.CreateAsync(task, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async System.Threading.Tasks.Task UpdateAsync(CompetitionManager.Core.Domains.Tasks.Task task, CancellationToken cancellationToken)
        {
            await _taskRepository.UpdateAsync(task, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _taskRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async System.Threading.Tasks.Task<List<Domains.Tasks.Task>> GetByNameAsync(string namePart, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(namePart)) return new List<Domains.Tasks.Task>();
            return await _taskRepository.GetByNameAsync(namePart, cancellationToken);
        }
    }
}
