using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;
using Threads = System.Threading.Tasks;

namespace TaskTracker.Services
{
    public class UserService : IUserService
    {
        private readonly TrackerDbContext _dbContext;

        public UserService(TrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(User user)
        {
            if (user == null) 
            { 
                throw new ArgumentNullException(nameof(user));
            }

            var responseUser = await _dbContext.Users.AddAsync(user);
            _ = await _dbContext.SaveChangesAsync();
            
            return responseUser.Entity;
        }

        public async Task<User> GetUserById(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                throw new ItemByIdNotFoundException(userId);
            }

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userList = await _dbContext.Users.ToListAsync();

            if (userList == null)
            {
                throw new Exception($"Task list is empty");
            }

            return userList;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            if (email == string.Empty)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                throw new ItemByStringNotFoundException(email);
            }

            return user;
        }

        public async Threads.Task DeleteUserById(int userId)
        {
            var user = GetUserById(userId);

            if (user == null)
            {
                throw new ItemByIdNotFoundException(userId);
            }

            _dbContext.Users.Remove(user.Result);
            _ = await _dbContext.SaveChangesAsync();
        }

        public async Threads.Task UpdateUser(User user)
        {
            var originalUser = GetUserById(user.UserId);

            if (originalUser == null)
            {
                throw new ItemByIdNotFoundException(user.UserId);
            }
            
            originalUser.Result.Name = user.Name;
            originalUser.Result.Password = user.Password;
            originalUser.Result.Email = user.Email;

            _ = await _dbContext.SaveChangesAsync();
        }
    }
}
