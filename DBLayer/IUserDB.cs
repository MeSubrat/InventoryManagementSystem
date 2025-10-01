using Common.Lib;
namespace DBLayer
{
    public interface IUserDB
    {
        bool CreateUser(UserModel user);
        bool UpdateUser(string userId,UserModel userToUpdate);
        bool DeleteUser(string userId);
        UserModel ViewUserById(string userId);
        UserModel GetUsername(string username);
        List<UserModel> GetAllUsers();
    }
}
