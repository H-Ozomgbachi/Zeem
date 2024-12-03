namespace ProductService.Domain.Pagination.QueryParams
{
    public class ProductParams : BaseParam
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
