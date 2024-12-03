namespace ProductService.Tests.UnitTests.Queries
{
    public class GetProductsQueryTests(TestFixture testFixture) : IClassFixture<TestFixture>
    {
        private readonly TestFixture _testFixture = testFixture;

        [Fact]
        public async Task Handle_GetProductsQuery_Success()
        {
            // Arrange
            IUnitOfWork unitOfWork = _testFixture.ServiceProvider.GetRequiredService<IUnitOfWork>();
            IMapper mapper = _testFixture.ServiceProvider.GetRequiredService<IMapper>();

            GetProductsQueryHandler handler = new(unitOfWork, mapper);

            GetProductsQuery query = new()
            {
                ProductParams = new()
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            // Act
            BaseResponse<PagedList<ProductModel>> result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(ResponseCodes.Ok, result.StatusCode);
            Assert.NotNull(result.Data);
        }
    }
}
