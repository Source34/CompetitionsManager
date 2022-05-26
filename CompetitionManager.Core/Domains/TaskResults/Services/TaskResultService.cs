using CompetitionManager.Core.Domains.TaskResults.Repositories;
using CompetitionManager.Core.Seriveces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionManager.Core.Domains.TaskResults.Services
{
    public class TaskResultService : ITaskResultService
    {
        private readonly ITaskResultRepository _taskResultRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskResultService(ITaskResultRepository taskResultRepository, IUnitOfWork unitOfWork)
        {
            _taskResultRepository = taskResultRepository;
            _unitOfWork = unitOfWork;
        }
        public Task<TaskResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _taskResultRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<List<TaskResult>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _taskResultRepository.GetAllAsync(cancellationToken);
        }

        public async Task CreateAsync(TaskResult taskResult, CancellationToken cancellationToken)
        {
            await _taskResultRepository.CreateAsync(taskResult, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TaskResult taskResult, CancellationToken cancellationToken)
        {
            await _taskResultRepository.UpdateAsync(taskResult, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _taskResultRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
