using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.PublisherBooks;

public interface IPublisherBookService
{
    Task<PublisherBook?> GetAsync(
        Expression<Func<PublisherBook, bool>> predicate,
        Func<IQueryable<PublisherBook>, IIncludableQueryable<PublisherBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<PublisherBook>?> GetListAsync(
        Expression<Func<PublisherBook, bool>>? predicate = null,
        Func<IQueryable<PublisherBook>, IOrderedQueryable<PublisherBook>>? orderBy = null,
        Func<IQueryable<PublisherBook>, IIncludableQueryable<PublisherBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<PublisherBook> AddAsync(PublisherBook publisherBook);
    Task<PublisherBook> UpdateAsync(PublisherBook publisherBook);
    Task<PublisherBook> DeleteAsync(PublisherBook publisherBook, bool permanent = false);
}
