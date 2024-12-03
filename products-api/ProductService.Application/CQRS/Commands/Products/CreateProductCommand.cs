namespace ProductService.Application.CQRS.Commands.Products
{
    public record CreateProductCommand : IRequest<BaseResponse<ProductModel>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CurrencyEnums Currency { get; set; }
        public int Stock { get; set; }
    }
}