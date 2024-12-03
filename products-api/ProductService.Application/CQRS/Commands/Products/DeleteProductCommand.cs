namespace ProductService.Application.CQRS.Commands.Products
{
    public record DeleteProductCommand : IRequest<BaseResponse<Unit>>
    {
        public Guid Id { get; set; }
    }
}
