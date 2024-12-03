namespace ProductService.Tests.UnitTests.Commands
{
    public class DeleteProductCommandTests(TestFixture testFixture) : IClassFixture<TestFixture>
    {
        private readonly TestFixture _testFixture = testFixture;

        [Fact]
        public async Task Handle_DeleteProduct_Success()
        {
            // Arrange
            IUnitOfWork unitOfWork = _testFixture.ServiceProvider.GetRequiredService<IUnitOfWork>();

            DeleteProductCommandHandler handler = new(unitOfWork);

            DeleteProductCommand deleteProductCommand = new()
            {
                Id = Guid.Parse("B35B8C16-61F1-4E19-88C7-08DD135FFEB9"),
            };

            // Act
            BaseResponse<Unit> result = await handler.Handle(deleteProductCommand, CancellationToken.None);

            // Assert
            Assert.Equal(ResponseCodes.Ok, result.StatusCode);
        }

        [Fact]
        public async Task Handle_DeleteProduct_NotFound()
        {
            // Arrange
            IUnitOfWork unitOfWork = _testFixture.ServiceProvider.GetRequiredService<IUnitOfWork>();

            DeleteProductCommandHandler handler = new(unitOfWork);

            DeleteProductCommand deleteProductCommand = new()
            {
                Id = Guid.NewGuid(), // Replace with invalid Id
            };

            // Act
            BaseResponse<Unit> result = await handler.Handle(deleteProductCommand, CancellationToken.None);

            // Assert
            Assert.Equal(ResponseCodes.NotFound, result.StatusCode);
        }
    }
}
