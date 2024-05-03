using Application.Features.LanguageBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LanguageBooks;

public class LanguageBookManager : ILanguageBookService
{
    private readonly ILanguageBookRepository _languageBookRepository;
    private readonly LanguageBookBusinessRules _languageBookBusinessRules;

    public LanguageBookManager(ILanguageBookRepository languageBookRepository, LanguageBookBusinessRules languageBookBusinessRules)
    {
        _languageBookRepository = languageBookRepository;
        _languageBookBusinessRules = languageBookBusinessRules;
    }

    public async Task<LanguageBook?> GetAsync(
        Expression<Func<LanguageBook, bool>> predicate,
        Func<IQueryable<LanguageBook>, IIncludableQueryable<LanguageBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LanguageBook? languageBook = await _languageBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return languageBook;
    }

    public async Task<IPaginate<LanguageBook>?> GetListAsync(
        Expression<Func<LanguageBook, bool>>? predicate = null,
        Func<IQueryable<LanguageBook>, IOrderedQueryable<LanguageBook>>? orderBy = null,
        Func<IQueryable<LanguageBook>, IIncludableQueryable<LanguageBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LanguageBook> languageBookList = await _languageBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return languageBookList;
    }

    public async Task<LanguageBook> AddAsync(LanguageBook languageBook)
    {
        LanguageBook addedLanguageBook = await _languageBookRepository.AddAsync(languageBook);

        return addedLanguageBook;
    }

    public async Task<LanguageBook> UpdateAsync(LanguageBook languageBook)
    {
        LanguageBook updatedLanguageBook = await _languageBookRepository.UpdateAsync(languageBook);

        return updatedLanguageBook;
    }

    public async Task<LanguageBook> DeleteAsync(LanguageBook languageBook, bool permanent = false)
    {
        LanguageBook deletedLanguageBook = await _languageBookRepository.DeleteAsync(languageBook);

        return deletedLanguageBook;
    }
}
