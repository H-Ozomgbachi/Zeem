namespace ProductService.Application.CQRS.Queries.Products
{
    public record GetProductsQuery : IRequest<BaseResponse<PagedList<ProductModel>>>
    {
        public required ProductParams ProductParams { get; set; }
    }
}
