using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Subjects;

public interface ISubjectService
{
    Task<Subject?> GetAsync(
        Expression<Func<Subject, bool>> predicate,
        Func<IQueryable<Subject>, IIncludableQueryable<Subject, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Subject>?> GetListAsync(
        Expression<Func<Subject, bool>>? predicate = null,
        Func<IQueryable<Subject>, IOrderedQueryable<Subject>>? orderBy = null,
        Func<IQueryable<Subject>, IIncludableQueryable<Subject, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Subject> AddAsync(Subject subject);
    Task<Subject> UpdateAsync(Subject subject);
    Task<Subject> DeleteAsync(Subject subject, bool permanent = false);
}
