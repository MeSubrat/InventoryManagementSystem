using InventoryManagementStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementStudio.DB
{
    public interface IProductDB
    {
        //Creat
        bool AddProduct(ProductModel product);
        //Read
        List<ProductModel> ViewAllProduct();
        //Read a product By Id
        ProductModel ViewAProductById(string productId);
        List<ProductModel> ViewAllProducts(string productId);
        List<ProductModel> ViewProductsById(string productId);
        //Delete Product.
        bool DeleteProduct(string productId);
        //Update Product.
        bool UpdateProduct(string productId, ProductModel productToUpdate);
    }
}
