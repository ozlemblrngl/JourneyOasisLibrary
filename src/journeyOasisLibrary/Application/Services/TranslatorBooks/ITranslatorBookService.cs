using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TranslatorBooks;

public interface ITranslatorBookService
{
    Task<TranslatorBook?> GetAsync(
        Expression<Func<TranslatorBook, bool>> predicate,
        Func<IQueryable<TranslatorBook>, IIncludableQueryable<TranslatorBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<TranslatorBook>?> GetListAsync(
        Expression<Func<TranslatorBook, bool>>? predicate = null,
        Func<IQueryable<TranslatorBook>, IOrderedQueryable<TranslatorBook>>? orderBy = null,
        Func<IQueryable<TranslatorBook>, IIncludableQueryable<TranslatorBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<TranslatorBook> AddAsync(TranslatorBook translatorBook);
    Task<TranslatorBook> UpdateAsync(TranslatorBook translatorBook);
    Task<TranslatorBook> DeleteAsync(TranslatorBook translatorBook, bool permanent = false);
}
