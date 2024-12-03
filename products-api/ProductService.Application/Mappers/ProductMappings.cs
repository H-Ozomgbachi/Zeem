namespace ProductService.Application.Mappers
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => UtilityHelper.ShouldMapMember(srcMember)));

            CreateMap<Product, UpdateProductCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => UtilityHelper.ShouldMapMember(srcMember)));

            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}