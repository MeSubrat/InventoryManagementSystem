using InventoryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Controller
{
    public interface IController
    {
        bool AddProduct(ProductModel product);
        bool DeleteProduct(string productId);
        List<ProductModel> GetAllProducts();
        bool UpdateProduct(string productId, ProductModel productToUpdate);
    }
}
