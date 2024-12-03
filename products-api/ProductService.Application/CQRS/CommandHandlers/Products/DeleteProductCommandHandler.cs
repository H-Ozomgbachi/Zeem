namespace ProductService.Application.CQRS.CommandHandlers.Products
{
    public class DeleteProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, BaseResponse<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BaseResponse<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _unitOfWork.ProductRepository.GetByPrimaryKey(request.Id, cancellationToken);

            if (product == null)
                return BaseResponse<Unit>.NotFound("Product not found");

            product.DeleteAudit("Henry"); // Ideally, this should be the current user ID

            // For soft delete
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return BaseResponse<Unit>.Success(Unit.Value, "Product deleted successfully");
        }
    }
}
