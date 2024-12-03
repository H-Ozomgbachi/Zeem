namespace ProductService.Tests.UnitTests.Commands
{
    public class UpdateProductCommandTests(TestFixture testFixture) : IClassFixture<TestFixture>
    {
        private readonly TestFixture _testFixture = testFixture;

        [Fact]
        public async Task Handle_UpdateProduct_Success()
        {
            // Arrange
            IUnitOfWork unitOfWork = _testFixture.ServiceProvider.GetRequiredService<IUnitOfWork>();
            IMapper mapper = _testFixture.ServiceProvider.GetRequiredService<IMapper>();

            UpdateProductCommandHandler handler = new(unitOfWork, mapper);

            UpdateProductCommand updateProductCommand = new()
            {
                Id = Guid.Parse("00D6CD80-2F68-4252-0028-08DD135F0419"), // Replace with valid Id
                Name = "Updated Zeem Product",
                Description = "Updated Zeem Product Description",
                Price = 200,
                Currency = CurrencyEnums.USD,
                Stock = 200,
            };

            // Act
            BaseResponse<ProductModel> result = await handler.Handle(updateProductCommand, CancellationToken.None);

            // Assert
            Assert.Equal(ResponseCodes.Ok, result.StatusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Handle_UpdateProduct_NotFound()
        {
            // Arrange
            IUnitOfWork unitOfWork = _testFixture.ServiceProvider.GetRequiredService<IUnitOfWork>();
            IMapper mapper = _testFixture.ServiceProvider.GetRequiredService<IMapper>();

            UpdateProductCommandHandler handler = new(unitOfWork, mapper);

            UpdateProductCommand updateProductCommand = new()
            {
                Id = Guid.NewGuid(), // Replace with invalid Id
                Name = "Updated Zeem Product",
                Description = "Updated Zeem Product Description",
                Price = 200,
                Currency = CurrencyEnums.USD,
                Stock = 200,
            };

            // Act
            BaseResponse<ProductModel> result = await handler.Handle(updateProductCommand, CancellationToken.None);

            // Assert
            Assert.Equal(ResponseCodes.NotFound, result.StatusCode);
            Assert.Null(result.Data);
        }
    }
}
