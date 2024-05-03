using Application.Features.Writers.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Writers;

public class WriterManager : IWriterService
{
    private readonly IWriterRepository _writerRepository;
    private readonly WriterBusinessRules _writerBusinessRules;

    public WriterManager(IWriterRepository writerRepository, WriterBusinessRules writerBusinessRules)
    {
        _writerRepository = writerRepository;
        _writerBusinessRules = writerBusinessRules;
    }

    public async Task<Writer?> GetAsync(
        Expression<Func<Writer, bool>> predicate,
        Func<IQueryable<Writer>, IIncludableQueryable<Writer, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Writer? writer = await _writerRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return writer;
    }

    public async Task<IPaginate<Writer>?> GetListAsync(
        Expression<Func<Writer, bool>>? predicate = null,
        Func<IQueryable<Writer>, IOrderedQueryable<Writer>>? orderBy = null,
        Func<IQueryable<Writer>, IIncludableQueryable<Writer, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Writer> writerList = await _writerRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return writerList;
    }

    public async Task<Writer> AddAsync(Writer writer)
    {
        Writer addedWriter = await _writerRepository.AddAsync(writer);

        return addedWriter;
    }

    public async Task<Writer> UpdateAsync(Writer writer)
    {
        Writer updatedWriter = await _writerRepository.UpdateAsync(writer);

        return updatedWriter;
    }

    public async Task<Writer> DeleteAsync(Writer writer, bool permanent = false)
    {
        Writer deletedWriter = await _writerRepository.DeleteAsync(writer);

        return deletedWriter;
    }
}
