using Application.Features.AnalogueBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AnalogueBooks;

public class AnalogueBookManager : IAnalogueBookService
{
    private readonly IAnalogueBookRepository _analogueBookRepository;
    private readonly AnalogueBookBusinessRules _analogueBookBusinessRules;

    public AnalogueBookManager(IAnalogueBookRepository analogueBookRepository, AnalogueBookBusinessRules analogueBookBusinessRules)
    {
        _analogueBookRepository = analogueBookRepository;
        _analogueBookBusinessRules = analogueBookBusinessRules;
    }

    public async Task<AnalogueBook?> GetAsync(
        Expression<Func<AnalogueBook, bool>> predicate,
        Func<IQueryable<AnalogueBook>, IIncludableQueryable<AnalogueBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AnalogueBook? analogueBook = await _analogueBookRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return analogueBook;
    }

    public async Task<IPaginate<AnalogueBook>?> GetListAsync(
        Expression<Func<AnalogueBook, bool>>? predicate = null,
        Func<IQueryable<AnalogueBook>, IOrderedQueryable<AnalogueBook>>? orderBy = null,
        Func<IQueryable<AnalogueBook>, IIncludableQueryable<AnalogueBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AnalogueBook> analogueBookList = await _analogueBookRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return analogueBookList;
    }

    public async Task<AnalogueBook> AddAsync(AnalogueBook analogueBook)
    {
        AnalogueBook addedAnalogueBook = await _analogueBookRepository.AddAsync(analogueBook);

        return addedAnalogueBook;
    }

    public async Task<AnalogueBook> UpdateAsync(AnalogueBook analogueBook)
    {
        AnalogueBook updatedAnalogueBook = await _analogueBookRepository.UpdateAsync(analogueBook);

        return updatedAnalogueBook;
    }

    public async Task<AnalogueBook> DeleteAsync(AnalogueBook analogueBook, bool permanent = false)
    {
        AnalogueBook deletedAnalogueBook = await _analogueBookRepository.DeleteAsync(analogueBook);

        return deletedAnalogueBook;
    }
}
