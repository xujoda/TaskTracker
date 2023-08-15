using Task = TaskTracker.Models.Task;
using User = TaskTracker.Models.User;

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

    public interface IUserService
    {
        User AddUser(User user);
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        List<User> GetAllUsers();
        void DeleteUserById(int userId);
        void UpdateUser(User user);
    }
}
