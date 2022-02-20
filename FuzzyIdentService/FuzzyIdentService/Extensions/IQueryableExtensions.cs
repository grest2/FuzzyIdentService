using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Models;
using Microsoft.EntityFrameworkCore;

namespace FuzzyIdentService.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<Row<T>> ToRowAsync<T>(this IQueryable<T> source, long count = 20, long offset = 0)
        {
            var query = source as IQueryable<T>;
            var total = await query.CountAsync();

            if (offset >= total)
            {
                return new Row<T>
                {
                    Total = total,
                    Offset = total,
                    Count = 0,
                    List = new List<T>(),
                };
            }

            if (total <= offset + count)
            {
                count = total - offset;
            }

            return new Row<T>
            {
                Total = total,
                Count = count,
                Offset = offset,
                List = await source.Skip((int) offset).Take((int) count).ToListAsync(),
            };
        }
    }
}