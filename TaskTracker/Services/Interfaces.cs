using Task = TaskTracker.Models.Task;
namespace TaskTracker.Services
{
    public interface ITaskService
    {
        Task CreateTask(Task task);
        Task GetTaskById(int taskId);
        List<Task> GetAllTasks();
        void DeleteTaskById(int taskId);
        void UpdateTask(Task task);
    }
}
