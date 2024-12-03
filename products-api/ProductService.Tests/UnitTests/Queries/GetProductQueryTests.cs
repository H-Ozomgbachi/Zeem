namespace ProductService.Tests.UnitTests.Queries
{
    public class GetProductQueryTests(TestFixture testFixture) : IClassFixture<TestFixture>
    {
        private readonly TestFixture _testFixture = testFixture;

        [Fact]
        public async Task Handle_GetProductQuery_Success()
        {
            // Arrange
            IUnitOfWork unitOfWork = _testFixture.ServiceProvider.GetRequiredService<IUnitOfWork>();
            IMapper mapper = _testFixture.ServiceProvider.GetRequiredService<IMapper>();

            GetProductQueryHandler handler = new(unitOfWork, mapper);

            GetProductQuery query = new()
            {
                Id = Guid.Parse("00D6CD80-2F68-4252-0028-08DD135F0419")
            };

            // Act
            BaseResponse<ProductModel> result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(ResponseCodes.Ok, result.StatusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Handle_GetProductQuery_NotFound()
        {
            // Arrange
            IUnitOfWork unitOfWork = _testFixture.ServiceProvider.GetRequiredService<IUnitOfWork>();
            IMapper mapper = _testFixture.ServiceProvider.GetRequiredService<IMapper>();

            GetProductQueryHandler handler = new(unitOfWork, mapper);

            GetProductQuery query = new()
            {
                Id = Guid.NewGuid() // Replace with non-existing product id
            };

            // Act
            BaseResponse<ProductModel> result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(ResponseCodes.NotFound, result.StatusCode);
            Assert.Null(result.Data);
        }
    }
}
