﻿namespace ApiCrud.apı.DTOs.Products
{
    public class UpdateProductDTo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double price { get; set; }
        public int CategoryId { get; set; }
    }
}
