using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SubjectBooks;

public interface ISubjectBookService
{
    Task<SubjectBook?> GetAsync(
        Expression<Func<SubjectBook, bool>> predicate,
        Func<IQueryable<SubjectBook>, IIncludableQueryable<SubjectBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<SubjectBook>?> GetListAsync(
        Expression<Func<SubjectBook, bool>>? predicate = null,
        Func<IQueryable<SubjectBook>, IOrderedQueryable<SubjectBook>>? orderBy = null,
        Func<IQueryable<SubjectBook>, IIncludableQueryable<SubjectBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<SubjectBook> AddAsync(SubjectBook subjectBook);
    Task<SubjectBook> UpdateAsync(SubjectBook subjectBook);
    Task<SubjectBook> DeleteAsync(SubjectBook subjectBook, bool permanent = false);
}
