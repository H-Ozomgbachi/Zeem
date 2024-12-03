namespace ProductService.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork(SqlDbContext sqlDbContext) : IUnitOfWork
    {
        private readonly SqlDbContext _sqlDbContext = sqlDbContext;
        private IDbContextTransaction? _transaction;
        private IProductRepository? _productRepository;

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(_sqlDbContext.Products);
                return _productRepository;
            }
        }

        public async Task BeginTransactionAsync(CancellationToken cancellation)
        {
            _transaction = await _sqlDbContext.Database.BeginTransactionAsync(cancellation);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _sqlDbContext.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            _sqlDbContext.Dispose();
            _transaction?.Dispose();
        }
    }
}
