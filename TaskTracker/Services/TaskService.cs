using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Services
{
    public class TaskService : ITaskService
    {
        private readonly TrackerDbContext _dbContext;

        public TaskService(TrackerDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Task> CreateTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var responseTask = await _dbContext.Tasks.AddAsync(task);
            _ = await _dbContext.SaveChangesAsync();

            return responseTask.Entity;
        }

        public async Task<Task> GetTaskById(int taskId)
        {
            if (taskId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(taskId));
            }

            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);

            if (task == null)
            {
                throw new ItemByIdNotFoundException(taskId);
            }

            return task;
        }

        public async Task<List<Task>> GetAllTasks()
        {
            var taskList = await _dbContext.Tasks.ToListAsync();

            if (taskList == null)
            {
                throw new Exception($"Task list is empty");
            }

            return taskList;
        }

        public async void UpdateTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var originalTask = await GetTaskById(task.TaskId);

            if (originalTask == null)
            {
                throw new ItemByIdNotFoundException(task.TaskId);
            }

            originalTask.Title = task.Title;
            originalTask.Description = task.Description;
            originalTask.Status = task.Status;
            originalTask.User = task.User;
            originalTask.Deadline = task.Deadline;
            originalTask.Updated = DateTime.Now;
            originalTask.Priority = task.Priority;
            originalTask.UserId = task.UserId;

            _ = await _dbContext.SaveChangesAsync();
        }

        public async void DeleteTaskById(int taskId)
        {
            var originalTask = await GetTaskById(taskId);

            if (originalTask == null)
            {
                throw new ItemByIdNotFoundException(taskId);
            }

            _dbContext.Tasks.Remove(originalTask);
            _ = await _dbContext.SaveChangesAsync();
        }
    }
}
