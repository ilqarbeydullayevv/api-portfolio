namespace ApiCrud.apı.DTOs.Products
{
    public class CreateProductDTo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double price { get; set; }
        public int CategoryId { get; set; }
    }
}
