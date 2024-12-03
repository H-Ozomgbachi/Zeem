namespace ProductService.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CurrencyEnums Currency { get; set; }
        public int Stock { get; set; }
    }
}