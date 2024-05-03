using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Writers;

public interface IWriterService
{
    Task<Writer?> GetAsync(
        Expression<Func<Writer, bool>> predicate,
        Func<IQueryable<Writer>, IIncludableQueryable<Writer, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Writer>?> GetListAsync(
        Expression<Func<Writer, bool>>? predicate = null,
        Func<IQueryable<Writer>, IOrderedQueryable<Writer>>? orderBy = null,
        Func<IQueryable<Writer>, IIncludableQueryable<Writer, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Writer> AddAsync(Writer writer);
    Task<Writer> UpdateAsync(Writer writer);
    Task<Writer> DeleteAsync(Writer writer, bool permanent = false);
}
