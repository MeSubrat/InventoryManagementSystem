using InventoryManagementStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementStudio.Controller
{
    public interface IController
    {
        bool AddProduct(ProductModel product);
        bool DeleteProduct(string productId);
        List<ProductModel> GetAllProducts();
        bool UpdateProduct(string productId, ProductModel productToUpdate);
    }
}
