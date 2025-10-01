using Common.Lib;
namespace BusinessLayer
{
    public interface IController
    {
        bool AddProduct(ProductModel product);
        bool DeleteProduct(string productId);
        List<ProductModel> GetAllProducts();
        bool UpdateProduct(string productId, ProductModel productToUpdate);
    }
}
