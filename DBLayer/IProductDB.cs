using Common.Lib;
namespace DBLayer
{
    public interface IProductDB
    {
        //Create
        bool AddProduct(ProductModel product);
        //Read
        List<ProductModel> ViewAllProduct();
        //Read a product By Id
        ProductModel ViewAProductById(string productId);
        //View all products if you are an admin
        List<ProductModel> ViewAllProducts(string productId);
        //View products of any user[Only for admin access]
        List<ProductModel> ViewProductsByUserId(string userId);
        //Delete Product.
        bool DeleteProduct(string productId);
        //Update Product.
        bool UpdateProduct(string productId, ProductModel productToUpdate);
    }
}
