using InventoryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DB
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
