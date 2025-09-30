using System.Collections.Generic;

namespace InventoryManagementSystem.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
        public bool isAdmin { get; set; } = false;

        public UserModel(string id,string username, string password,bool isAdmin)
        {
            Id = id;
            Username = username;
            Password = password;    
            this.isAdmin = isAdmin;
        }
        public override string ToString()
        {
            return $"User Id: {Id}, Username: {Username},No of Products: {Products.Count},Is Admin:{isAdmin} ";
        }
    }
}
