namespace ProductService.Application.CQRS.QueryHandlers.Products
{
    public class GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductsQuery, BaseResponse<PagedList<ProductModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedList<ProductModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            PagedList<Product> products = await _unitOfWork.ProductRepository.GetProducts(request.ProductParams, cancellationToken);

            return new()
            {
                Message = "Products retrieved successfully",
                Data = PagedListHelper<ProductModel>.MapToList(products, _mapper),
                MetaData = PagedListHelper<Product>.GetPaginationInfo(products)
            };
        }
    }
}
