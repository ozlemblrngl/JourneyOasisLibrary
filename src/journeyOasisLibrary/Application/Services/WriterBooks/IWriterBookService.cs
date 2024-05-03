using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.WriterBooks;

public interface IWriterBookService
{
    Task<WriterBook?> GetAsync(
        Expression<Func<WriterBook, bool>> predicate,
        Func<IQueryable<WriterBook>, IIncludableQueryable<WriterBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<WriterBook>?> GetListAsync(
        Expression<Func<WriterBook, bool>>? predicate = null,
        Func<IQueryable<WriterBook>, IOrderedQueryable<WriterBook>>? orderBy = null,
        Func<IQueryable<WriterBook>, IIncludableQueryable<WriterBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<WriterBook> AddAsync(WriterBook writerBook);
    Task<WriterBook> UpdateAsync(WriterBook writerBook);
    Task<WriterBook> DeleteAsync(WriterBook writerBook, bool permanent = false);
}
