namespace ProductService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Add AutoMapper
            services.AddAutoMapper(typeof(ProductMappings).Assembly);

            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetProductQuery).Assembly));

            // Add FluentValidation
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();

            // Others
            services.AddOptions<AppSettings>().Bind(config.GetSection("AppSettings"));
            services.AddSingleton<UtilityHelper>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
