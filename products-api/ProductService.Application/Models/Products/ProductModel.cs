namespace ProductService.Application.Models.Products
{
    public record ProductModel : BaseModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CurrencyEnums Currency { get; set; }
        public int Stock { get; set; }
    }
}
