namespace ProductService.Application.CQRS.CommandHandlers.Products
{
    public class UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateProductCommand, BaseResponse<ProductModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<ProductModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _unitOfWork.ProductRepository.GetByPrimaryKey(request.Id, cancellationToken);

            if (product == null)
                return BaseResponse<ProductModel>.NotFound("Product not found");

            _mapper.Map(request, product);

            product.UpdateAudit("Henry"); // Ideally, this should be the logged-in user ID

            EntityEntry<Product> entry = _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BaseResponse<ProductModel>.Success(_mapper.Map<ProductModel>(entry.Entity), "Product updated successfully");
        }
    }
}
