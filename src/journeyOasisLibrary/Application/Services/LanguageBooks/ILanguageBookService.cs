using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LanguageBooks;

public interface ILanguageBookService
{
    Task<LanguageBook?> GetAsync(
        Expression<Func<LanguageBook, bool>> predicate,
        Func<IQueryable<LanguageBook>, IIncludableQueryable<LanguageBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<LanguageBook>?> GetListAsync(
        Expression<Func<LanguageBook, bool>>? predicate = null,
        Func<IQueryable<LanguageBook>, IOrderedQueryable<LanguageBook>>? orderBy = null,
        Func<IQueryable<LanguageBook>, IIncludableQueryable<LanguageBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<LanguageBook> AddAsync(LanguageBook languageBook);
    Task<LanguageBook> UpdateAsync(LanguageBook languageBook);
    Task<LanguageBook> DeleteAsync(LanguageBook languageBook, bool permanent = false);
}
