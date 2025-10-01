using Common.Lib;
using DBLayer;
namespace Authentication.Lib
{
    public class UserAuthentication
    {
        private readonly IUserDB _userDb;
        public UserAuthentication(IUserDB userDb)
        {
            _userDb = userDb;
        }

        public UserModel Login(string username,string password)
        {
            var existingUser = _userDb.GetUsername(username);
            if(existingUser!=null && VerifyPassword(existingUser.Password, password))
            {
                Console.WriteLine("Login successfull!");
                return existingUser;
            }
            Console.WriteLine("User not registered or Entered credentials are wrong!");
            return null;
        }
        public bool Signup(string username,string password,bool isAdmin)
        {
            var existingUser = _userDb.GetUsername(username);
            if (existingUser != null)
            {
                Console.WriteLine("User Already exist...Please login");
                return false;
            }
            string userId = Guid.NewGuid().ToString();
            UserModel newUser = new UserModel(userId,username, password, isAdmin);
            _userDb.CreateUser(newUser);
            return true;
        }

        private bool VerifyPassword(string registeredPassword, string enteredPassword)
        {
            return registeredPassword == enteredPassword;
        }
    }
}
