using System.Collections.Generic;

namespace InventoryManagementStudio.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ProductModel> Products { get; set; } = null;
        public override string ToString()
        {
            return $"User Id: {Id}, User Name: {Name},No of Products: {Products.Count}";
        }
    }
}
