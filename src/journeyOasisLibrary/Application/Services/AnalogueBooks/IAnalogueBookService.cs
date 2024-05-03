using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AnalogueBooks;

public interface IAnalogueBookService
{
    Task<AnalogueBook?> GetAsync(
        Expression<Func<AnalogueBook, bool>> predicate,
        Func<IQueryable<AnalogueBook>, IIncludableQueryable<AnalogueBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<AnalogueBook>?> GetListAsync(
        Expression<Func<AnalogueBook, bool>>? predicate = null,
        Func<IQueryable<AnalogueBook>, IOrderedQueryable<AnalogueBook>>? orderBy = null,
        Func<IQueryable<AnalogueBook>, IIncludableQueryable<AnalogueBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<AnalogueBook> AddAsync(AnalogueBook analogueBook);
    Task<AnalogueBook> UpdateAsync(AnalogueBook analogueBook);
    Task<AnalogueBook> DeleteAsync(AnalogueBook analogueBook, bool permanent = false);
}
