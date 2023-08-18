using Task = TaskTracker.Models.Task;
using User = TaskTracker.Models.User;
using Threading = System.Threading.Tasks;

namespace TaskTracker.Services
{
    public interface ITaskService
    {
        Task<Task> CreateTask(Task task);
        Task<Task> GetTaskById(int taskId);
        Task<List<Task>> GetAllTasks();
        Threading.Task DeleteTaskById(int taskId);
        Threading.Task UpdateTask(Task task);
    }

    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string email);
        Task <List<User>> GetAllUsers();
        Threading.Task DeleteUserById(int userId);
        Threading.Task UpdateUser(User user);
    }
}
