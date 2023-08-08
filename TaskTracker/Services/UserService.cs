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

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public User GetUserById(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                throw new ItemByIdNotFoundException(userId);
            }

            return user;
        }

        public List<User> GetAllUsers()
        {
            List<User> userList = _dbContext.Users.ToList();

            if (userList == null)
            {
                throw new Exception($"Task list is empty");
            }

            return userList;
        }

        public User GetUserByEmail(string email)
        {
            if (email == string.Empty)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                throw new ItemByStringNotFoundException(email);
            }

            return user;
        }

        public void DeleteUserById(int userId)
        {
            var user = GetUserById(userId);

            if (user == null)
            {
                throw new ItemByIdNotFoundException(userId);
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var originalUser = GetUserById(user.UserId);

            if (user == null)
            {
                throw new ItemByIdNotFoundException(user.UserId);
            }

            originalUser.Name = user.Name;
            originalUser.Password = user.Password;
            originalUser.Email = user.Email;

            _dbContext.SaveChanges();
        }
    }
}
