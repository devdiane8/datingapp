using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class PaginatedResult<T>
{
    public PaginationMetadata Metadata { get; set; } = default!;
    public List<T> Items { get; set; } = default!;

}

public class PaginationMetadata
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
};

public class PaginationHelper {
    public static async Task<PaginatedResult<T>> CreateAsync<T>(IQueryable<T> query, int page, int pageSize)
    {
        var count = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedResult<T>
        {
            Metadata = new PaginationMetadata
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = count
            },
            Items = items
        };
    }
}

