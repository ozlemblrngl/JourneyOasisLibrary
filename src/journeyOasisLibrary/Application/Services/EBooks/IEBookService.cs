using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.EBooks;

public interface IEBookService
{
    Task<EBook?> GetAsync(
        Expression<Func<EBook, bool>> predicate,
        Func<IQueryable<EBook>, IIncludableQueryable<EBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<EBook>?> GetListAsync(
        Expression<Func<EBook, bool>>? predicate = null,
        Func<IQueryable<EBook>, IOrderedQueryable<EBook>>? orderBy = null,
        Func<IQueryable<EBook>, IIncludableQueryable<EBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<EBook> AddAsync(EBook eBook);
    Task<EBook> UpdateAsync(EBook eBook);
    Task<EBook> DeleteAsync(EBook eBook, bool permanent = false);
}
