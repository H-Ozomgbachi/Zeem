namespace ProductService.API
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration config)
        {
            #region Swagger Setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Zeem Products API", Version = "v1" });
                c.OperationFilter<ApplySummariesOperationFilter>();
                c.SchemaFilter<EnumSchemaFilter>();
                c.EnableAnnotations();
            });
            #endregion

            #region API Versioning Setup
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("api-version"));
            });
            #endregion

            #region Security Related Region
            services.AddSingleton<IStartupFilter, SecurityHeadersStartupFilter>();
            #endregion

            #region Miscellaneous Setup
            services.AddEndpointsApiExplorer();
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationExceptionFilter>();
                options.SuppressAsyncSuffixInActionNames = false;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            string[] origins = config["AppSettings:Origins"]?.Split(',') ?? [];
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins(origins)
                .AllowAnyMethod().AllowAnyHeader());
            });
            #endregion

            return services;
        }
    }
}
