using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BookFormats;

public interface IBookFormatService
{
    Task<BookFormat?> GetAsync(
        Expression<Func<BookFormat, bool>> predicate,
        Func<IQueryable<BookFormat>, IIncludableQueryable<BookFormat, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<BookFormat>?> GetListAsync(
        Expression<Func<BookFormat, bool>>? predicate = null,
        Func<IQueryable<BookFormat>, IOrderedQueryable<BookFormat>>? orderBy = null,
        Func<IQueryable<BookFormat>, IIncludableQueryable<BookFormat, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<BookFormat> AddAsync(BookFormat bookFormat);
    Task<BookFormat> UpdateAsync(BookFormat bookFormat);
    Task<BookFormat> DeleteAsync(BookFormat bookFormat, bool permanent = false);
}
