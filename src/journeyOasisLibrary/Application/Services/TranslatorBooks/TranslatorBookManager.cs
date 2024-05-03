using Application.Features.TranslatorBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TranslatorBooks;

public class TranslatorBookManager : ITranslatorBookService
{
    private readonly ITranslatorBookRepository _translatorBookRepository;
    private readonly TranslatorBookBusinessRules _translatorBookBusinessRules;

    public TranslatorBookManager(ITranslatorBookRepository translatorBookRepository, TranslatorBookBusinessRules translatorBookBusinessRules)
    {
        _translatorBookRepository = translatorBookRepository;
        _translatorBookBusinessRules = translatorBookBusinessRules;
    }

    public async Task<TranslatorBook?> GetAsync(
        Expression<Func<TranslatorBook, bool>> predicate,
        Func<IQueryable<TranslatorBook>, IIncludableQueryable<TranslatorBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        TranslatorBook? translatorBook = await _translatorBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return translatorBook;
    }

    public async Task<IPaginate<TranslatorBook>?> GetListAsync(
        Expression<Func<TranslatorBook, bool>>? predicate = null,
        Func<IQueryable<TranslatorBook>, IOrderedQueryable<TranslatorBook>>? orderBy = null,
        Func<IQueryable<TranslatorBook>, IIncludableQueryable<TranslatorBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<TranslatorBook> translatorBookList = await _translatorBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return translatorBookList;
    }

    public async Task<TranslatorBook> AddAsync(TranslatorBook translatorBook)
    {
        TranslatorBook addedTranslatorBook = await _translatorBookRepository.AddAsync(translatorBook);

        return addedTranslatorBook;
    }

    public async Task<TranslatorBook> UpdateAsync(TranslatorBook translatorBook)
    {
        TranslatorBook updatedTranslatorBook = await _translatorBookRepository.UpdateAsync(translatorBook);

        return updatedTranslatorBook;
    }

    public async Task<TranslatorBook> DeleteAsync(TranslatorBook translatorBook, bool permanent = false)
    {
        TranslatorBook deletedTranslatorBook = await _translatorBookRepository.DeleteAsync(translatorBook);

        return deletedTranslatorBook;
    }
}
