using App.Domain.Core.Contract.Task;
using Task = App.Domain.Core.Entites.Task;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.AppService
{
    public class TaskAppService : ITaskAppService
    {
        private readonly ITaskService _taskService;

        public TaskAppService(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<int> Create(int id, Task task, CancellationToken cancellationToken)
        {
            try
            {
                return await _taskService.Add(id, task, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Add: {ex.Message}");
            }
        }

        public async Task<List<Task>> GeTasks(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _taskService.GeTasks(id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error List: {ex.Message}");
            }
        }

        public async Task<bool> Update(int userId,int taskId, Task updateTask, CancellationToken cancellationToken)
        {
            try
            {
                return await _taskService.Update(userId, taskId, updateTask, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Update: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int userId, int taskId, CancellationToken cancellationToken)
        {
            try
            {
                return await _taskService.Delete(userId, taskId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Delete: {ex.Message}");
            }
        }

        public async Task<int> GetIsCompleted(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _taskService.GetIsCompleted(id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Delete: {ex.Message}");
            }
        }

        public async Task<bool> ChangeStatus(int userId, int taskId, bool newStatus, CancellationToken cancellationToken)
        {
            try
            {
                return await _taskService.ChangeStatus(userId, taskId, newStatus, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ChangeStatus: {ex.Message}");
            }
        }

        public async Task<Task> GetTaskById(int userId, int taskId, CancellationToken cancellationToken)
        {
            try
            {
                return await _taskService.GetTaskById(userId, taskId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ChangeStatus: {ex.Message}");
            }
        }
    }
}