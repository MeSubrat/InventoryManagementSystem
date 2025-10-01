using Common.Lib;
namespace DBLayer
{
    public class UserDb : IUserDB
    {
        private static List<UserModel> users = new List<UserModel>();
        public bool CreateUser(UserModel user)
        {
            if(user != null)
            {
                users.Add(user);
                return true;
            }
            return false;
        }
        public bool DeleteUser(string userId)
        {
            UserModel userToBeDeleted = users.Find(user => user.Id == userId);
            if(userToBeDeleted != null)
            {
                users.Remove(userToBeDeleted);
                return true;
            }
            return false;
        }
        public bool UpdateUser(string userId, UserModel userToUpdate)
        {
            UserModel existingUser = users.Find(user => user.Id == userId);
            if (existingUser != null)
            {
                existingUser.Id = userToUpdate.Id;
                existingUser.Username = userToUpdate.Username;
                return true;
            }
            return false;
        }
        public UserModel ViewUserById(string userId)
        {
            var existingUser = users.Find(user => user.Id == userId);
            return existingUser;
        }
        public UserModel GetUsername(string username)
        {
            var existingUser = users.Find(user => user.Username == username);
            return existingUser;
        }
        public List<UserModel> GetAllUsers()
        {
            return users;
        }
    }
}
