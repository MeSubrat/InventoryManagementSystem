using InventoryManagementSystem.DB;
using InventoryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Controller
{
    internal class InventoryController : IController
    {
        private readonly IProductDB _productDb;
        private readonly UserModel _currentUser;
        private readonly IUserDB _userDb;
        public InventoryController(IProductDB productDB, IUserDB userDb,UserModel currentUser)
        {
            _productDb = productDB;
            _currentUser = currentUser;
            _userDb = userDb;
        }
        //Controllers related to products
        public bool AddProduct(ProductModel product)
        {
            product.OwnerUserId = _currentUser.Id;
            product.Id = Guid.NewGuid().ToString();
            var result = _productDb.AddProduct(product);
            if (result)
            {
                Console.WriteLine($"Product Added successfully..Generated product id: {product.Id}");
            }
            return result;
        }
        public bool DeleteProduct(string productId)
        {
            var product = _productDb.ViewAProductById(productId);
            var result = false;
            if (product!=null && (_currentUser.isAdmin == true || _currentUser.Id == product.OwnerUserId))
            {
                result = _productDb.DeleteProduct(productId);
            }
            if(result == true)
            {
                Console.WriteLine($"{productId} deleted successfully!");
            }
            else
            {
                Console.WriteLine($"Error while deleting {productId}");
            }
            return result;
        }

        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> products;
            if (_currentUser.isAdmin == true)
            {
                products = _productDb.ViewAllProduct();
            }
            else
            {
                //products = _productDb.ViewProductsById(_currentUser.Id);
                products = null;
                Console.WriteLine("To see other's products, user must be an admin!");
            }
            return products;
        }
        public ProductModel GetProduct(string productId)
        {
            //return _productDb.ViewAProductById(productId);
            ProductModel requiredProduct =  _productDb.ViewAProductById(productId);
            if(requiredProduct.OwnerUserId == _currentUser.Id || _currentUser.isAdmin == true)
            {
                return _productDb.ViewAProductById(productId);
            }
            Console.WriteLine("Unauthorised access!");
            return null;
        }
        public List<ProductModel> GetProductsByUserId(string userId)
        {
            List<ProductModel> products = _productDb.ViewProductsByUserId(_currentUser.Id);
            return products;
        }

        public bool UpdateProduct(string productId, ProductModel productToUpdate)
        {
            var existingProduct = _productDb.ViewAProductById(productId);
            var result = false;
            if(_currentUser.isAdmin == true || existingProduct.OwnerUserId == _currentUser.Id)
            {
                _productDb.UpdateProduct(productId,productToUpdate);
                result = true; 
            }
            return result;
        }
        //Controllers related to User...
        public bool AddUser(UserModel user)
        {
            if(_currentUser.isAdmin == true)
            {
                var result = _userDb.CreateUser(user);
                if(result == true)
                {
                    Console.WriteLine("User adedd successfully!");
                }
                return result;
            }
            Console.WriteLine("You must be an admin to Add an user!");
            return false;
        }
        public UserModel GetUser(string userId)
        {
            if(_currentUser.isAdmin == true || _currentUser.Id == userId)
            {
                var user = _userDb.ViewUserById(userId);
                return user;
            }
            else
            {
                Console.WriteLine("Unauthorised access!");
                return null;
            }
        }
        public List<UserModel> GetAllUsers()
        {
            if(_currentUser.isAdmin == true)
            {
                return _userDb.GetAllUsers();
            }
            else
            {
                Console.WriteLine("Unauthorised Access!");
                return null;
            }
        }
        public bool UpdateUser(string userId,UserModel userToUpdate)
        {
            if(_currentUser.isAdmin == true || _currentUser.Id == userId)
            {
                var result = _userDb.UpdateUser(userId, userToUpdate);
                return result;
            }
            Console.WriteLine("Unauthorised access!");
            return false;
            
        }
        public bool DeleteUser(string userId)
        {
            if(_currentUser.isAdmin == true || _currentUser.Id == userId)
            {
                var result = _userDb.DeleteUser(userId);
                return result;
            }
            Console.WriteLine("Unauthorised access!");
            return false;
        }
    }
}
