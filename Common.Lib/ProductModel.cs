namespace Common.Lib
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string OwnerUserId { get; set; }

        public ProductModel(string name,double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public override string ToString()
        {
            return $"ProductId: {Id}, Product Name: {Name}, Product Price: {Price},Available Quantity: {Quantity}, Created By: {OwnerUserId}";
        }
    }
}
