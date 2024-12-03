namespace ProductService.Infrastructure.Persistence.EntityConfigurations
{
    public abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedBy)
            .HasMaxLength(100);

            builder.Property(e => e.ModifiedBy)
            .HasMaxLength(100);

            builder.Property(e => e.DeletedBy)
            .HasMaxLength(100);
        }
    }
}
