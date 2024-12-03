namespace ProductService.Application.CQRS.QueryHandlers.Products
{
    public class GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductQuery, BaseResponse<ProductModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ProductModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _unitOfWork.ProductRepository.GetByPrimaryKey(request.Id, cancellationToken);

            if (product == null)
                return BaseResponse<ProductModel>.NotFound("Product not found");

            return BaseResponse<ProductModel>.Success(_mapper.Map<ProductModel>(product), "Product retrieved successfully");
        }
    }
}
