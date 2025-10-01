using Common.Lib;
using DBLayer;

namespace DBLayer
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
        public List<ProductModel> ViewProductsByUserId(string userId)
        {
            List<ProductModel> results = new List<ProductModel>();
            foreach(var product in products)
            {
                if(product.OwnerUserId == userId)
                {
                    results.Add(product);
                }
            }
            return results;
        }
    }
}
