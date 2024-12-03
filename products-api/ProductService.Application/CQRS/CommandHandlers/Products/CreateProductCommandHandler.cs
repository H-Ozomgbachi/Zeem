namespace ProductService.Application.CQRS.CommandHandlers.Products
{
    public class CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateProductCommand, BaseResponse<ProductModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ProductModel>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);

            bool isProductExist = await _unitOfWork.ProductRepository.ExistAsync([x => x.Name == product.Name], cancellationToken);

            if (isProductExist)
            {
                return BaseResponse<ProductModel>.BadRequest("Product already exist");
            }

            EntityEntry<Product> entry = await _unitOfWork.ProductRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BaseResponse<ProductModel>.Success(_mapper.Map<ProductModel>(entry.Entity), "Product created successfully");
        }
    }
}