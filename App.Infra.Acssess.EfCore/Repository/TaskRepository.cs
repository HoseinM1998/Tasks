using App.Domain.Core.Contract.Task;
using App.Infra.Db.Sql;
using Microsoft.EntityFrameworkCore;
using App.Domain.Core.Entites;
using Task = App.Domain.Core.Entites.Task;


namespace App.Infra.Acssess.EfCore.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> Add(int userId, Domain.Core.Entites.Task task, CancellationToken cancellationToken)
        {
            task.UserId = userId;
            await _context.Tasks.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return task.Id;
        }


        public async Task<List<Domain.Core.Entites.Task>> GeTasks(int id, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Where(t => t.UserId == id)
                .OrderByDescending(t => t.CreatAt)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }


        public async Task<bool> Update(int userId, int taskId, Domain.Core.Entites.Task updateTask, CancellationToken cancellationToken)
        {
            var taskToUpdate = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
            if (taskToUpdate.UserId != userId)
            {
                throw new UnauthorizedAccessException("شما مجاز به ویرایش این تسک نیستید.");
            }
            taskToUpdate.Title = updateTask.Title;
            taskToUpdate.Description = updateTask.Description;
            await _context.SaveChangesAsync(cancellationToken);
            return true; 
        }


        public async Task<bool> Delete(int userId, int taskId, CancellationToken cancellationToken)
        {
            var taskToDelete = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);

            if (taskToDelete == null)
            {
                throw new Exception("وظیفه یافت نشد ");
            }
            if (taskToDelete.UserId != userId)
            {
                throw new UnauthorizedAccessException("شما مجاز به حذف این وظیفه نیستید");
            }
            _context.Tasks.Remove(taskToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


        public async Task<int> GetIsCompleted(int userId, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId && t.IsCompleted == false)
                .AsNoTracking()
                .CountAsync(cancellationToken);
        }

        public async Task<bool> ChangeStatus(int userId, int taskId, bool newStatus, CancellationToken cancellationToken)
        {
            var changeStatus = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);

            if (changeStatus == null)
            {
                throw new Exception("تسک یافت نشد.");
            }
            if (changeStatus.UserId != userId)
            {
                throw new UnauthorizedAccessException("شما مجاز به ویرایش این تسک نیستید.");
            }

            changeStatus.IsCompleted = newStatus;
            _context.Tasks.Update(changeStatus);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Domain.Core.Entites.Task> GetTaskById(int userId, int taskId, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks
                .Where(t => t.Id == taskId && t.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (task == null)
            {
                throw new Exception("وظیفه یافت نشد یا شما مجاز به مشاهده آن نیستید");
            }

            return task;
        }
    }
}