using App.Domain.Core.Contract.Task;
using App.Domain.Core.Entites;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Task = App.Domain.Core.Entites.Task;
using App.Domain.Core.Entites.Config;
using Microsoft.Extensions.Configuration;

namespace App.Domain.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly SiteSetting _siteSetting;
        private readonly IConfiguration _configuration;

        public TaskService(ITaskRepository repository, SiteSetting siteSetting, IConfiguration configuration)
        {
            _repository = repository;
            _siteSetting = siteSetting;
            _configuration = configuration;
        }

        public async Task<int> Add(int id, Core.Entites.Task task, CancellationToken cancellationToken)
        {
            var limit = int.Parse(_configuration.GetSection("LimitTask:TaskUnfinished").Value);
            var count = await _repository.GetIsCompleted(id, cancellationToken);
            if (limit < count)
            {
                throw new Exception($"Limited");
            }
            
            task.IsCompleted = false;
            task.CreatAt= DateTime.Now;
            task.UserId= id;
            return await _repository.Add(id,task, cancellationToken);
        }

        public async Task<List<Core.Entites.Task>> GeTasks(int id, CancellationToken cancellationToken)
        {
            return await _repository.GeTasks(id, cancellationToken);
        }

        public async Task<bool> Update(int userId,int taskId, Core.Entites.Task updateTask, CancellationToken cancellationToken)
        {
            return await _repository.Update(userId, taskId, updateTask, cancellationToken);
        }

        public async Task<bool> Delete(int userId,int taskId, CancellationToken cancellationToken)
        {
            return await _repository.Delete(userId, taskId, cancellationToken);
        }

        public async Task<int> GetIsCompleted(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetIsCompleted(id, cancellationToken);
        }

        public async Task<bool> ChangeStatus(int userId, int taskId, bool newStatus, CancellationToken cancellationToken)
        {
            return await _repository.ChangeStatus(userId, taskId, newStatus,cancellationToken);
        }

        public async Task<Task> GetTaskById(int userId, int taskId, CancellationToken cancellationToken)
        {
            return await _repository.GetTaskById(userId, taskId, cancellationToken);

        }
    }
}