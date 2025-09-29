using InventoryManagementStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementStudio.DB
{
    public class ProductDb : IProductDB
    {
        private List<ProductModel> products = new List<ProductModel>();
        public bool AddProduct(ProductModel product)
        {
            if (product == null) return false;
            products.Add(product);
            return true;
        }

        public bool DeleteProduct(string productId)
        {
            ProductModel productToDelete = products.Find(item => item.Id == productId);
            if (productToDelete == null)
            {
                Console.WriteLine("Product not found!");
                return false;
            }
            products.Remove(productToDelete);
            return true;
        }

        public bool UpdateProduct(string productId, ProductModel productToUpdate)
        {
            ProductModel existingProduct = products.Find(item => item.Id == productId);
            if (existingProduct == null)
            {
                Console.WriteLine("Product not found!");
                return false;
            }
            existingProduct.Id = productToUpdate.Id;
            existingProduct.Name = productToUpdate.Name;
            existingProduct.Price = productToUpdate.Price;
            existingProduct.OwnerUserId = productToUpdate.OwnerUserId;
            existingProduct.Quantity = productToUpdate.Quantity;
            return true;
        }

        public List<ProductModel> ViewAllProduct()
        {
            return products;
        }
        //Only Accessible for Admin
        public ProductModel ViewAProductById(string productId)
        {
            ProductModel requiredProduct = products.Find(item => item.Id == productId);
            return requiredProduct;
        }
        //Only Accessible for Admin
        public List<ProductModel> ViewAllProducts(string productId)
        {
            return products;
        }
        public List<ProductModel> ViewProductsById(string productId)
        {
            List<ProductModel> results = new List<ProductModel>();
            foreach(var product in products)
            {
                if(product.Id == productId)
                {
                    results.Add(product);
                }
            }
            return results;
        }
    }
}
