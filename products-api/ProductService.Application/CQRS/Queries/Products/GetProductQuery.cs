namespace ProductService.Application.CQRS.Queries.Products
{
    public record GetProductQuery : IRequest<BaseResponse<ProductModel>>
    {
        public Guid Id { get; set; }
    }
}
