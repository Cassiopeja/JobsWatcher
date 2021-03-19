using System;
using System.Collections.Generic;

namespace JobsWatcher.Core.Entities
{
    public class PagedItems<T>
    {
        public PagedItems()
        {
        }

        public PagedItems(List<T> data)
        {
            TotalCount = data.Count;
            PageSize = data.Count;
            PageNumber = 1;
            TotalPages = 1;
            Data = data;
        }

        public PagedItems(List<T> data, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            Data = data;
        }

        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}