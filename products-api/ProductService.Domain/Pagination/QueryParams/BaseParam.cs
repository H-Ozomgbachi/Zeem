namespace ProductService.Domain.Pagination.QueryParams
{
    public abstract class BaseParam
    {
        private readonly int _maxPageSize = 100;
        private int _pageSize = 50;

        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; } = 1;

        [JsonProperty("pageSize")]
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
        }
    }
}
