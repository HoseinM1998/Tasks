using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Contract.Task
{
    public interface ITaskAppService
    {
        Task<int> Create(int id, Core.Entites.Task task, CancellationToken cancellationToken);
        Task<List<Core.Entites.Task>> GeTasks(int id, CancellationToken cancellationToken);
        Task<bool> Update(int userId, int taskId, Core.Entites.Task updateTask, CancellationToken cancellationToken);
        Task<bool> Delete(int userId, int taskId, CancellationToken cancellationToken);
        Task<int> GetIsCompleted(int id, CancellationToken cancellationToken);
        Task<bool> ChangeStatus(int userId, int taskId, bool newStatus, CancellationToken cancellationToken);
        Task<Core.Entites.Task> GetTaskById(int userId, int taskId, CancellationToken cancellationToken);

    }
}
