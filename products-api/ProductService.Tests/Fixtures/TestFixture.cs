namespace ProductService.Tests.Fixtures
{
    public class TestFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        private readonly IConfiguration _configuration;

        public TestFixture()
        {
            IServiceCollection services = new ServiceCollection();
            _configuration = TestConfiguration.GetConfiguration();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            ConfigureApplicationServices(services);
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentException("Database connection string is null in Tests");

            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
            services.AddSingleton(_configuration);
            services.AddAutoMapper(typeof(ProductMappings).Assembly);
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetProductQuery).Assembly));
            services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
            services.AddLogging();
            services.AddSingleton<UtilityHelper>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
    public class TestConfiguration
    {
        public static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
