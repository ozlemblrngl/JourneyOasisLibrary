using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Formats;

public interface IFormatService
{
    Task<Format?> GetAsync(
        Expression<Func<Format, bool>> predicate,
        Func<IQueryable<Format>, IIncludableQueryable<Format, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Format>?> GetListAsync(
        Expression<Func<Format, bool>>? predicate = null,
        Func<IQueryable<Format>, IOrderedQueryable<Format>>? orderBy = null,
        Func<IQueryable<Format>, IIncludableQueryable<Format, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Format> AddAsync(Format format);
    Task<Format> UpdateAsync(Format format);
    Task<Format> DeleteAsync(Format format, bool permanent = false);
}
