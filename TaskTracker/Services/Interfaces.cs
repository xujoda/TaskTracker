using Task = TaskTracker.Models.Task;
using User = TaskTracker.Models.User;

namespace TaskTracker.Services
{
    public interface ITaskService
    {
        Task<Task> CreateTask(Task task);
        Task<Task> GetTaskById(int taskId);
        Task<List<Task>> GetAllTasks();
        void DeleteTaskById(int taskId);
        void UpdateTask(Task task);
    }

    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string email);
        Task <List<User>> GetAllUsers();
        void DeleteUserById(int userId);
        void UpdateUser(User user);
    }
}
