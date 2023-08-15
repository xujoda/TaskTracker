using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class UserService : IUserService
    {
        private readonly TrackerDbContext _dbContext;

        public UserService(TrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User AddUser(User user)
        {
            if (user == null) 
            { 
                throw new ArgumentNullException(nameof(user));
            }

            _dbContext.Users.AddAsync(user);
            _dbContext.SaveChangesAsync();
            return user;
        }

        public User GetUserById(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var user = _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user.Result == null)
            {
                throw new ItemByIdNotFoundException(userId);
            }

            return user.Result;
        }

        public List<User> GetAllUsers()
        {
            var userList = _dbContext.Users.ToListAsync();

            if (userList.Result == null)
            {
                throw new Exception($"Task list is empty");
            }

            return userList.Result;
        }

        public User GetUserByEmail(string email)
        {
            if (email == string.Empty)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var user = _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user.Result == null)
            {
                throw new ItemByStringNotFoundException(email);
            }

            return user.Result;
        }

        public void DeleteUserById(int userId)
        {
            var user = GetUserById(userId);

            if (user == null)
            {
                throw new ItemByIdNotFoundException(userId);
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            var originalUser = GetUserById(user.UserId);

            if (originalUser == null)
            {
                throw new ItemByIdNotFoundException(user.UserId);
            }

            originalUser.Name = user.Name;
            originalUser.Password = user.Password;
            originalUser.Email = user.Email;

            _dbContext.SaveChangesAsync();
        }
    }
}
