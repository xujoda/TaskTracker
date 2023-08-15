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

        public Task CreateTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _dbContext.Tasks.AddAsync(task);
            _dbContext.SaveChangesAsync();

            return task;
        }

        public Task GetTaskById(int taskId)
        {
            if (taskId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(taskId));
            }

            var task = _dbContext.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);

            if (task.Result == null)
            {
                throw new ItemByIdNotFoundException(taskId);
            }

            return task.Result;
        }

        public List<Task> GetAllTasks()
        {
            var taskList = _dbContext.Tasks.ToListAsync();

            if (taskList.Result == null)
            {
                throw new Exception($"Task list is empty");
            }

            return taskList.Result;
        }

        public void UpdateTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var originalTask = GetTaskById(task.TaskId);

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

            _dbContext.SaveChangesAsync();
        }

        public void DeleteTaskById(int taskId)
        {
            var originalTask = GetTaskById(taskId);

            if (originalTask == null)
            {
                throw new ItemByIdNotFoundException(taskId);
            }

            _dbContext.Tasks.Remove(originalTask);
            _dbContext.SaveChangesAsync();
        }
    }
}
