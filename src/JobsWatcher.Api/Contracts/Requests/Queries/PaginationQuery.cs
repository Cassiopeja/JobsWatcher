namespace JobsWatcher.Api.Contracts.Requests.Queries
{
    public class PaginationQuery
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;
        protected int _maxItemsPerPage;

        public PaginationQuery()
        {
            _maxItemsPerPage = 100;
            PageNumber = 1;
            PageSize = _maxItemsPerPage;
        }

        public int PageNumber
        {
            get => _pageNumber;
            init => _pageNumber = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            init
            {
                if (value < 1)
                    _pageSize = 1;
                else if (value > _maxItemsPerPage)
                    _pageSize = _maxItemsPerPage;
                else
                    _pageSize = value;
            }
        }
    }
}