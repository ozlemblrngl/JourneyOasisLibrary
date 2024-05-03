using Application.Features.Formats.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Formats;

public class FormatManager : IFormatService
{
    private readonly IFormatRepository _formatRepository;
    private readonly FormatBusinessRules _formatBusinessRules;

    public FormatManager(IFormatRepository formatRepository, FormatBusinessRules formatBusinessRules)
    {
        _formatRepository = formatRepository;
        _formatBusinessRules = formatBusinessRules;
    }

    public async Task<Format?> GetAsync(
        Expression<Func<Format, bool>> predicate,
        Func<IQueryable<Format>, IIncludableQueryable<Format, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Format? format = await _formatRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return format;
    }

    public async Task<IPaginate<Format>?> GetListAsync(
        Expression<Func<Format, bool>>? predicate = null,
        Func<IQueryable<Format>, IOrderedQueryable<Format>>? orderBy = null,
        Func<IQueryable<Format>, IIncludableQueryable<Format, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Format> formatList = await _formatRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return formatList;
    }

    public async Task<Format> AddAsync(Format format)
    {
        Format addedFormat = await _formatRepository.AddAsync(format);

        return addedFormat;
    }

    public async Task<Format> UpdateAsync(Format format)
    {
        Format updatedFormat = await _formatRepository.UpdateAsync(format);

        return updatedFormat;
    }

    public async Task<Format> DeleteAsync(Format format, bool permanent = false)
    {
        Format deletedFormat = await _formatRepository.DeleteAsync(format);

        return deletedFormat;
    }
}
