namespace ProductService.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(DbSet<Product> products) : RepositoryBase<Product>(products), IProductRepository
    {
        private readonly DbSet<Product> _products = products;

        public async Task<PagedList<Product>> GetProducts(ProductParams @params, CancellationToken cancellationToken)
        {
            IQueryable<Product> query = _products.AsNoTracking().Where(p => p.IsDeleted == false);

            if (!string.IsNullOrEmpty(@params.Name))
            {
                query = query.Where(p => p.Name.Contains(@params.Name));
            }

            if(@params.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= @params.MinPrice);
            }

            if(@params.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= @params.MaxPrice);
            }

            query = query.OrderByDescending(p => p.CreatedAt);

            return await PagedList<Product>.ToPagedListAsync(query, @params.PageNumber, @params.PageSize, cancellationToken);
        }
    }
}
