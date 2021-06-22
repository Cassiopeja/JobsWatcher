using System.Linq;
using System.Threading.Tasks;
using JobsWatcher.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Extensions
{
    public static class PaginationExtensions
    {
        public static async Task<PagedItems<T>> ToPagedItemsAsync<T>(this IQueryable<T> source, int pageNumber,
            int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedItems<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<PagedItems<T>> ToPagedItemsAsync<T>(this IQueryable<T> source)
        {
            var items = await source.ToListAsync();
            return new PagedItems<T>(items);
        }
        
        public static async Task<PagedItems<T>> ToPagedItemsAsync<T>(this IQueryable<T> source, PaginationFilter paginationFilter)
        {
            if (paginationFilter == null)
                return await ToPagedItemsAsync(source);

            return await ToPagedItemsAsync(source, paginationFilter.PageNumber, paginationFilter.PageSize);
        }
        
        
    }
}