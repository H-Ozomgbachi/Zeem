namespace ProductService.Infrastructure.Data.DbContexts
{
    public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("ZeemProducts");

            modelBuilder.ApplyConfiguration(new ProductConfig());
        }
    }
}
