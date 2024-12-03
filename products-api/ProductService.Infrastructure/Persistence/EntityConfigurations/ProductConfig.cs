namespace ProductService.Infrastructure.Persistence.EntityConfigurations
{
    public class ProductConfig : BaseEntityConfig<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.HasIndex(e => e.Name).IsUnique();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(e => e.Description)
                .HasMaxLength(2000);

            builder.Property(e => e.Price)
                .HasColumnType("decimal(14,2)");

            builder.Property(e => e.Currency)
                .IsRequired()
                .HasConversion<EnumToStringConverter<CurrencyEnums>>()
                .HasMaxLength(5);
        }
    }
}
