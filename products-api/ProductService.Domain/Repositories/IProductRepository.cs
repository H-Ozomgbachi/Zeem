namespace ProductService.Domain.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<PagedList<Product>> GetProducts(ProductParams @params, CancellationToken cancellationToken);
    }
}
