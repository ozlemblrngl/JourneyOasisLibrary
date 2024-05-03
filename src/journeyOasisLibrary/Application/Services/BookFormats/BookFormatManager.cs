using Application.Features.BookFormats.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.BookFormats;

public class BookFormatManager : IBookFormatService
{
    private readonly IBookFormatRepository _bookFormatRepository;
    private readonly BookFormatBusinessRules _bookFormatBusinessRules;

    public BookFormatManager(IBookFormatRepository bookFormatRepository, BookFormatBusinessRules bookFormatBusinessRules)
    {
        _bookFormatRepository = bookFormatRepository;
        _bookFormatBusinessRules = bookFormatBusinessRules;
    }

    public async Task<BookFormat?> GetAsync(
        Expression<Func<BookFormat, bool>> predicate,
        Func<IQueryable<BookFormat>, IIncludableQueryable<BookFormat, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BookFormat? bookFormat = await _bookFormatRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return bookFormat;
    }

    public async Task<IPaginate<BookFormat>?> GetListAsync(
        Expression<Func<BookFormat, bool>>? predicate = null,
        Func<IQueryable<BookFormat>, IOrderedQueryable<BookFormat>>? orderBy = null,
        Func<IQueryable<BookFormat>, IIncludableQueryable<BookFormat, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BookFormat> bookFormatList = await _bookFormatRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return bookFormatList;
    }

    public async Task<BookFormat> AddAsync(BookFormat bookFormat)
    {
        BookFormat addedBookFormat = await _bookFormatRepository.AddAsync(bookFormat);

        return addedBookFormat;
    }

    public async Task<BookFormat> UpdateAsync(BookFormat bookFormat)
    {
        BookFormat updatedBookFormat = await _bookFormatRepository.UpdateAsync(bookFormat);

        return updatedBookFormat;
    }

    public async Task<BookFormat> DeleteAsync(BookFormat bookFormat, bool permanent = false)
    {
        BookFormat deletedBookFormat = await _bookFormatRepository.DeleteAsync(bookFormat);

        return deletedBookFormat;
    }
}
