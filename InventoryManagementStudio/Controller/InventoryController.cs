using InventoryManagementStudio.DB;
using InventoryManagementStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementStudio.Controller
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
                Console.WriteLine($"Product Added successfully..Generated user id: {product.Id}");
            }
            return result;
        }
        public bool DeleteProduct(string productId)
        {
            var product = _productDb.ViewAProductById(productId);
            var result = false;
            if (_currentUser.isAdmin == true || _currentUser.Id == product.Id)
            {
                result = _productDb.DeleteProduct(productId);
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
        public List<ProductModel> GetProductsByUserId(string userId)
        {
            List<ProductModel> products = _productDb.ViewProductsById(_currentUser.Id);
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
    }
}
