using Application.Features.EBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.EBooks;

public class EBookManager : IEBookService
{
    private readonly IEBookRepository _eBookRepository;
    private readonly EBookBusinessRules _eBookBusinessRules;

    public EBookManager(IEBookRepository eBookRepository, EBookBusinessRules eBookBusinessRules)
    {
        _eBookRepository = eBookRepository;
        _eBookBusinessRules = eBookBusinessRules;
    }

    public async Task<EBook?> GetAsync(
        Expression<Func<EBook, bool>> predicate,
        Func<IQueryable<EBook>, IIncludableQueryable<EBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        EBook? eBook = await _eBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return eBook;
    }

    public async Task<IPaginate<EBook>?> GetListAsync(
        Expression<Func<EBook, bool>>? predicate = null,
        Func<IQueryable<EBook>, IOrderedQueryable<EBook>>? orderBy = null,
        Func<IQueryable<EBook>, IIncludableQueryable<EBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<EBook> eBookList = await _eBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return eBookList;
    }

    public async Task<EBook> AddAsync(EBook eBook)
    {
        EBook addedEBook = await _eBookRepository.AddAsync(eBook);

        return addedEBook;
    }

    public async Task<EBook> UpdateAsync(EBook eBook)
    {
        EBook updatedEBook = await _eBookRepository.UpdateAsync(eBook);

        return updatedEBook;
    }

    public async Task<EBook> DeleteAsync(EBook eBook, bool permanent = false)
    {
        EBook deletedEBook = await _eBookRepository.DeleteAsync(eBook);

        return deletedEBook;
    }
}
