namespace ProductService.Tests.UnitTests.Commands
{
    public class CreateProductCommandTests(TestFixture testFixture) : IClassFixture<TestFixture>
    {
        private readonly TestFixture _testFixture = testFixture;

        [Fact]
        public async Task Handle_CreateProduct_Success()
        {
            // Arrange
            IUnitOfWork unitOfWork = _testFixture.ServiceProvider.GetRequiredService<IUnitOfWork>();
            IMapper mapper = _testFixture.ServiceProvider.GetRequiredService<IMapper>();

            CreateProductCommandHandler handler = new(unitOfWork, mapper);

            CreateProductCommand createProductCommand = new()
            {
                Name = "Second Zeem Product",
                Description = "First Zeem Product Description",
                Price = 100,
                Currency = CurrencyEnums.NGN,
                Stock = 100,
            };

            // Act
            BaseResponse<ProductModel> result = await handler.Handle(createProductCommand, CancellationToken.None);

            // Assert
            Assert.Equal(ResponseCodes.Ok, result.StatusCode);
            Assert.NotNull(result.Data);
        }
    }
}
