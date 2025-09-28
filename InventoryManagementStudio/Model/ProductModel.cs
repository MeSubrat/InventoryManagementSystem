using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementStudio.Model
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string CreatedBy { get; set; }

        public ProductModel(string id,string name,double price, int quantity, string createdBy)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            CreatedBy = createdBy;
        }
        public override string ToString()
        {
            return $"ProductId: {Id}, Product Name: {Name}, Product Price: {Price},Available Quantity: {Quantity}, Created By: {CreatedBy}";
        }
    }
}
