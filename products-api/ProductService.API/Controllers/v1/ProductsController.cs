namespace ProductService.API.Controllers.v1
{
    public class ProductsController(ISender sender) : BaseControllerV1
    {
        private readonly ISender _sender = sender;

        [HttpGet]
        [ProducesResponseType<BaseResponse<PagedList<ProductModel>>>(200)]
        [SwaggerOperation(Summary = "Get all products")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductParams productParams, CancellationToken cancellation)
        {
            BaseResponse<PagedList<ProductModel>> response = await _sender.Send(new GetProductsQuery { ProductParams = productParams }, cancellation);
            return HandleResponse(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<BaseResponse<ProductModel>>(200)]
        [SwaggerOperation(Summary = "Get product by id")]
        public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellation)
        {
            BaseResponse<ProductModel> response = await _sender.Send(new GetProductQuery { Id = id }, cancellation);
            return HandleResponse(response);
        }

        [HttpPost]
        [ProducesResponseType<BaseResponse<ProductModel>>(200)]
        [SwaggerOperation(Summary = "Create a new product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command, CancellationToken cancellation)
        {
            BaseResponse<ProductModel> response = await _sender.Send(command, cancellation);
            return HandleResponse(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType<BaseResponse<ProductModel>>(200)]
        [SwaggerOperation(Summary = "Update a product")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellation)
        {
            command.Id = id;
            BaseResponse<ProductModel> response = await _sender.Send(command, cancellation);
            return HandleResponse(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType<BaseResponse<Unit>>(200)]
        [SwaggerOperation(Summary = "Delete a product")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellation)
        {
            BaseResponse<Unit> response = await _sender.Send(new DeleteProductCommand { Id = id }, cancellation);
            return HandleResponse(response);
        }
    }
}
