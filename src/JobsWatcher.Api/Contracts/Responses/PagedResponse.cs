using System.Collections.Generic;

namespace JobsWatcher.Api.Contracts.Responses
{
    public class PagedResponse<T>
    {
        public PagedResponse()
        {
        }

        public PagedResponse(List<T> data)
        {
            Data = data;
        }

        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}